using PhotoEmotion.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoEmotion.Web.Controllers
{
    public class HomeController : Controller
    {
        private PhotoEmotionWebContext db = new PhotoEmotionWebContext();

        // GET: Home
        public ActionResult Index()
        {
            var modelo = new Home();
            return View(modelo);
        }

        public ActionResult MostrarStats()
        {
            ViewBag.PictureCount = db.EmoPicture.Count();
            ViewBag.FaceCount = db.EmoFace.Count();
            ViewBag.EmotionCount = db.EmoEmotion.Count();
            ViewBag.OtroValor = "Hola";
            return View();
        }

        public ActionResult MostrarStatsRaw()
        {
            ViewBag.PictureCount = db.EmoPicture.Count();
            ViewBag.FaceCount = db.EmoFace.Count();
            ViewBag.EmotionCount = db.EmoEmotion.Count();
            ViewBag.OtroValor = "Hola";
            return View();
        }

    }
}