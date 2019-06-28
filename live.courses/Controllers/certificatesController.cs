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
    public class certificatesController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();
        
        [ResponseType(typeof(certificate))]
        public IHttpActionResult GetcertificateByInstId(string id)
        {

            var data = db.get_certificates_by_inst_id(id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // GET: api/certificates
        public IQueryable<certificate> Getcertificates()
        {
            return db.certificates;
        }

        // GET: api/certificates/5
        //[ResponseType(typeof(certificate))]
        //public IHttpActionResult Getcertificate(int id)
        //{
        //    certificate certificate = db.certificates.Find(id);
        //    if (certificate == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(certificate);
        //}

        // PUT: api/certificates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcertificate(int id, certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != certificate.id)
            {
                return BadRequest();
            }

            db.Entry(certificate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!certificateExists(id))
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

        // POST: api/certificates
        [ResponseType(typeof(certificate))]
        public IHttpActionResult Postcertificate(certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.certificates.Add(certificate);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = certificate.id }, certificate);
        }

        // DELETE: api/certificates/5
        [ResponseType(typeof(certificate))]
        public IHttpActionResult Deletecertificate(int id)
        {
            certificate certificate = db.certificates.Find(id);
            if (certificate == null)
            {
                return NotFound();
            }

            db.certificates.Remove(certificate);
            db.SaveChanges();

            return Ok(certificate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool certificateExists(int id)
        {
            return db.certificates.Count(e => e.id == id) > 0;
        }
    }
}