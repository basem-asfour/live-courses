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
    public class roomsController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();

        
        // GET: api/rooms
        public IQueryable<room> Getrooms()
        {
            return db.rooms;
        }

        // GET: api/rooms/5
        [ResponseType(typeof(room))]
        public IHttpActionResult Getroom(int id)
        {
            room room = db.rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putroom(int id, room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.id)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!roomExists(id))
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

        // POST: api/rooms
        [ResponseType(typeof(room))]
        public IHttpActionResult Postroom(room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Room_Members rm = new Room_Members();
            rm.Room_id = room.id;
            rm.Member = room.admin;
            rm.AddingDate = DateTime.Now;
            db.Room_Members.Add(rm);
            db.rooms.Add(room);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (roomExists(room.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = room.id }, room);
        }

        // DELETE: api/rooms/5
        [ResponseType(typeof(room))]
        public IHttpActionResult Deleteroom(int id)
        {
            room room = db.rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool roomExists(int id)
        {
            return db.rooms.Count(e => e.id == id) > 0;
        }
    }
}