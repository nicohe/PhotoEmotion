﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PhotoEmotion.Web.Models;

namespace PhotoEmotion.Web.Controllers
{
    public class EmoEmotionsAPIController : ApiController
    {
        private PhotoEmotionWebContext db = new PhotoEmotionWebContext();

        // GET: api/EmoEmotionsAPI
        public IQueryable<EmoEmotion> GetEmoEmotion()
        {
            return db.EmoEmotion;
        }

        // GET: api/EmoEmotionsAPI/5
        [ResponseType(typeof(EmoEmotion))]
        public IHttpActionResult GetEmoEmotion(int id)
        {
            EmoEmotion emoEmotion = db.EmoEmotion.Find(id);
            if (emoEmotion == null)
            {
                return NotFound();
            }

            return Ok(emoEmotion);
        }

        // PUT: api/EmoEmotionsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmoEmotion(int id, EmoEmotion emoEmotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emoEmotion.Id)
            {
                return BadRequest();
            }

            db.Entry(emoEmotion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmoEmotionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmoEmotionsAPI
        [ResponseType(typeof(EmoEmotion))]
        public IHttpActionResult PostEmoEmotion(EmoEmotion emoEmotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmoEmotion.Add(emoEmotion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = emoEmotion.Id }, emoEmotion);
        }

        // DELETE: api/EmoEmotionsAPI/5
        [ResponseType(typeof(EmoEmotion))]
        public IHttpActionResult DeleteEmoEmotion(int id)
        {
            EmoEmotion emoEmotion = db.EmoEmotion.Find(id);
            if (emoEmotion == null)
            {
                return NotFound();
            }

            db.EmoEmotion.Remove(emoEmotion);
            db.SaveChanges();

            return Ok(emoEmotion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmoEmotionExists(int id)
        {
            return db.EmoEmotion.Count(e => e.Id == id) > 0;
        }
    }
}