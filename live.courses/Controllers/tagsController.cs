using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using live.courses.Models;

namespace live.courses.Controllers
{
    public class tagsController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();

        // GET: api/tags
        public IQueryable<tag> Gettags()
        {
            return db.tags;
        }

        // GET: api/tags/5
        [ResponseType(typeof(tag))]
        public IHttpActionResult Gettag(int id)
        {
            tag tag = db.tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        // PUT: api/tags/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttag(int id, tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tag.id)
            {
                return BadRequest();
            }

            db.Entry(tag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tagExists(id))
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

        // POST: api/tags
        [ResponseType(typeof(tag))]
        public IHttpActionResult Posttag(tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tags.Add(tag);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tag.id }, tag);
        }

        // DELETE: api/tags/5
        [ResponseType(typeof(tag))]
        public IHttpActionResult Deletetag(int id)
        {
            tag tag = db.tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            db.tags.Remove(tag);
            db.SaveChanges();

            return Ok(tag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tagExists(int id)
        {
            return db.tags.Count(e => e.id == id) > 0;
        }
    }
}