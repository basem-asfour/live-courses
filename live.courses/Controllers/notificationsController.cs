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
using Microsoft.AspNet.SignalR.Client;

namespace live.courses.Controllers
{
    public class notificationsController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();
        private HubConnection _connection;
        private IHubProxy _myhub;


        //public string sendm(string message)
        //{
            
        //}
        // GET: api/notifications
        public IQueryable<notification> Getnotifications()
        {
            return db.notifications;
        }

        // GET: api/notifications/5
        [ResponseType(typeof(notification))]
        public IHttpActionResult Getnotification(int id)
        {
            notification notification = db.notifications.Find(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // PUT: api/notifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putnotification(int id, notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notification.id)
            {
                return BadRequest();
            }

            db.Entry(notification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!notificationExists(id))
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

        // POST: api/notifications
        [ResponseType(typeof(notification))]
        public IHttpActionResult Postnotification(notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.notifications.Add(notification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = notification.id }, notification);
        }

        // DELETE: api/notifications/5
        [ResponseType(typeof(notification))]
        public IHttpActionResult Deletenotification(int id)
        {
            notification notification = db.notifications.Find(id);
            if (notification == null)
            {
                return NotFound();
            }

            db.notifications.Remove(notification);
            db.SaveChanges();

            return Ok(notification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool notificationExists(int id)
        {
            return db.notifications.Count(e => e.id == id) > 0;
        }
    }
}