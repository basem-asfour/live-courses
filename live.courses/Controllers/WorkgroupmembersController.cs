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
    public class WorkgroupmembersController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();

        // GET: api/Workgroupmembers
        public IQueryable<Work_group_members> GetWork_group_members()
        {
            return db.Work_group_members;
        }

        // GET: api/Workgroupmembers/5
        [ResponseType(typeof(Work_group_members))]
        public IHttpActionResult GetWork_group_members(int id)
        {
            Work_group_members work_group_members = db.Work_group_members.Find(id);
            if (work_group_members == null)
            {
                return NotFound();
            }

            return Ok(work_group_members);
        }

        // PUT: api/Workgroupmembers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWork_group_members(int id, Work_group_members work_group_members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != work_group_members.Work_group_id)
            {
                return BadRequest();
            }

            db.Entry(work_group_members).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Work_group_membersExists(id))
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

        // POST: api/Workgroupmembers
        [ResponseType(typeof(Work_group_members))]
        public IHttpActionResult PostWork_group_members(Work_group_members work_group_members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Work_group_members.Add(work_group_members);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Work_group_membersExists(work_group_members.Work_group_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = work_group_members.Work_group_id }, work_group_members);
        }

        // DELETE: api/Workgroupmembers/5
        [ResponseType(typeof(Work_group_members))]
        public IHttpActionResult DeleteWork_group_members(int wg_id, string member_id)
        {
            Work_group_members work_group_members = db.Work_group_members.Find(wg_id,member_id);
            if (work_group_members == null)
            {
                return NotFound();
            }

            db.Work_group_members.Remove(work_group_members);
            db.SaveChanges();

            return Ok(work_group_members);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Work_group_membersExists(int id)
        {
            return db.Work_group_members.Count(e => e.Work_group_id == id) > 0;
        }
    }
}