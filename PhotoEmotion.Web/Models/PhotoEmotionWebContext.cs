using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoEmotion.Web.Models
{
    public class PhotoEmotionWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PhotoEmotionWebContext() : base("name=PhotoEmotionWebAzure")
        {
            Database.SetInitializer<PhotoEmotionWebContext>(new DropCreateDatabaseIfModelChanges<PhotoEmotionWebContext>());
        }

        public DbSet<EmoPicture> EmoPicture { get; set; }

        public DbSet<EmoFace> EmoFace { get; set; }

        public DbSet<EmoEmotion> EmoEmotion { get; set; }

        public DbSet<Home> Homes { get; set; }
    }
}
