using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoEmotion.Web.Models
{
    public class Home
    {
        public int Id { get; set; }
        public string WelcomeMessage { get; set; } = "Reconocedor de Emociones";
        public string Footer { get; set; } = "Footer by .Net";
    }
}