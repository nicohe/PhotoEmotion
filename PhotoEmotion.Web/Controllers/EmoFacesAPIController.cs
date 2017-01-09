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
    public class EmoFacesAPIController : ApiController
    {
        private PhotoEmotionWebContext db = new PhotoEmotionWebContext();

        // GET: api/EmoFacesAPI
        public IQueryable<EmoFace> GetEmoFace()
        {
            return db.EmoFace;
        }

        // GET: api/EmoFacesAPI/5
        [ResponseType(typeof(EmoFace))]
        public IHttpActionResult GetEmoFace(int id)
        {
            EmoFace emoFace = db.EmoFace.Find(id);
            if (emoFace == null)
            {
                return NotFound();
            }

            return Ok(emoFace);
        }

        // PUT: api/EmoFacesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmoFace(int id, EmoFace emoFace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emoFace.Id)
            {
                return BadRequest();
            }

            db.Entry(emoFace).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmoFaceExists(id))
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

        // POST: api/EmoFacesAPI
        [ResponseType(typeof(EmoFace))]
        public IHttpActionResult PostEmoFace(EmoFace emoFace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmoFace.Add(emoFace);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = emoFace.Id }, emoFace);
        }

        // DELETE: api/EmoFacesAPI/5
        [ResponseType(typeof(EmoFace))]
        public IHttpActionResult DeleteEmoFace(int id)
        {
            EmoFace emoFace = db.EmoFace.Find(id);
            if (emoFace == null)
            {
                return NotFound();
            }

            db.EmoFace.Remove(emoFace);
            db.SaveChanges();

            return Ok(emoFace);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmoFaceExists(int id)
        {
            return db.EmoFace.Count(e => e.Id == id) > 0;
        }
    }
}