using PhotoEmotion.Web.Models;
using PhotoEmotion.Web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhotoEmotion.Web.Controllers
{
    public class EmoUploaderController : Controller
    {
        string serverFoldePath;
        string key;
        EmotionHelper emoHelper;
        PhotoEmotionWebContext db = new PhotoEmotionWebContext();

        public EmoUploaderController()
        {
            serverFoldePath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            emoHelper = new EmotionHelper(key);
        }

        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmoUploader
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            if (file?.ContentLength > 0)
            {
                var pictureName = Guid.NewGuid().ToString();
                pictureName += Path.GetExtension(file.FileName);

                var route = Server.MapPath(serverFoldePath);

                route = route + "/" + pictureName;

                file.SaveAs(route);

                var emoPicture = await emoHelper.DetectAndExtractFacesAsync(file.InputStream);

                emoPicture.Name = file.FileName;
                emoPicture.Path = $"{serverFoldePath}/{pictureName}";

                db.EmoPicture.Add(emoPicture);

                await db.SaveChangesAsync();

                return RedirectToAction("Details", "EmoPictures", new { ID = emoPicture.Id });
            }

            return View();
        }
    }
}