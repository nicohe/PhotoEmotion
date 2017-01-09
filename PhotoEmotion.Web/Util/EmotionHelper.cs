﻿using Microsoft.ProjectOxford.Emotion.Contract;
using PhotoEmotion.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace PhotoEmotion.Web.Util
{
    public class EmotionHelper
    {
        public Microsoft.ProjectOxford.Emotion.EmotionServiceClient emoClient;

        public EmotionHelper(string key)
        {
            emoClient = new Microsoft.ProjectOxford.Emotion.EmotionServiceClient(key);
        }

        public async Task<EmoPicture> DetectAndExtractFacesAsync(Stream imageStream)
        {
            Emotion[] emotions = await emoClient.RecognizeAsync(imageStream);

            var emoPicture = new EmoPicture();

            emoPicture.Faces = ExtractFaces(emotions, emoPicture);

            return emoPicture;
        }

        // populate Faces of emoPicture
        private ObservableCollection<EmoFace> ExtractFaces(Emotion[] emotions, EmoPicture emoPicture)
        {
            var listaFaces = new ObservableCollection<EmoFace>();

            // llenamos la informacion de cada una de las caras que se encuentran en cada emocion
            foreach (var emotion in emotions)
            {
                var emoFace = new EmoFace()
                {
                    X = emotion.FaceRectangle.Left,
                    Y = emotion.FaceRectangle.Top,
                    Width = emotion.FaceRectangle.Width,
                    Height = emotion.FaceRectangle.Height,
                    Picture = emoPicture,

                };

                emoFace.Emotions = ProcessEmotions(emotion.Scores, emoFace);
                listaFaces.Add(emoFace);
            }

            return listaFaces;
        }

        private ObservableCollection<EmoEmotion> ProcessEmotions(Scores scores, EmoFace emoFace)
        {
            var emotionList = new ObservableCollection<EmoEmotion>();

            var properties = scores.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //var filterProperties = properties.Where(p => p.PropertyType == typeof(float));
            var filterProperties = from p in properties
                                   where p.PropertyType == typeof(float)
                                   select p;

            var emoType = EmoEmotionEnum.Undertermined;

            foreach (var prop in filterProperties)
            {
                if (!Enum.TryParse<EmoEmotionEnum>(prop.Name, out emoType))
                    emoType = EmoEmotionEnum.Undertermined;

                var emoEmotion = new EmoEmotion();
                emoEmotion.Score = (float)prop.GetValue(scores);
                emoEmotion.EmotionType = emoType;
                emoEmotion.Face = emoFace;

                emotionList.Add(emoEmotion);
            }

            return emotionList;
        }
    }
}