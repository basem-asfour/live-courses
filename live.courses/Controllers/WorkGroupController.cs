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
    public class WorkGroupController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();

        // Archive A work group
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult ArchiveGroup(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.archive_work_group(id);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!work_groupExists(id))
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

        //Un Archive A work group
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UnArchiveGroup(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.un_archive_work_group(id);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!work_groupExists(id))
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
        //get All archived work groups for A user
        public IEnumerable<get_all_archived_work_groups_for_user_Result> GetAllArchivedGroupsForUser(string id)
        {
            return db.get_all_archived_work_groups_for_user(id).ToList();
        }
        //get All Un archived work groups for A user
        public IEnumerable<get_all_un_archived_work_groups_for_user_Result> GetAllUnArchivedGroupsForUser(string id)
        {
            return db.get_all_un_archived_work_groups_for_user(id).ToList();
        }
        //get All archived work groups
        public IEnumerable<Get_all_archived_groups_Result> GetAllArchivedGroups()
        {
            return db.Get_all_archived_groups().ToList();
        }
        //get All un archived work groups
        public IEnumerable<Get_all_unarchived_groups_Result> GetAllUnArchivedGroups()
        {
            return db.Get_all_unarchived_groups().ToList();
        }
        //get all tags
        public IEnumerable<get_all_work_group_tags_Result> Gettags(int id)
        {
            return db.get_all_work_group_tags(id).ToList();
        }
        // GET: api/WorkGroup
        public IQueryable<work_group> Getwork_group()
        {
            return db.work_group;
        }

        // GET: api/WorkGroup/5
        [ResponseType(typeof(work_group))]
        public IHttpActionResult Getwork_group(int id)
        {
            work_group work_group = db.work_group.Find(id);
            if (work_group == null)
            {
                return NotFound();
            }

            return Ok(work_group);
        }

        // PUT: api/WorkGroup/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putwork_group(int id, work_group work_group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != work_group.id)
            {
                return BadRequest();
            }

            db.Entry(work_group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!work_groupExists(id))
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

        // POST: api/WorkGroup
        [ResponseType(typeof(work_group))]
        public IHttpActionResult Postwork_group(work_group work_group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Work_group_members wg = new Work_group_members();
            wg.Member_id = work_group.admin;
            wg.Work_group_id = work_group.id;
            wg.AddingDate = System.DateTime.Now;
            db.work_group.Add(work_group);
            db.Work_group_members.Add(wg);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = work_group.id }, work_group);
        }

        // DELETE: api/WorkGroup/5
        [ResponseType(typeof(work_group))]
        public IHttpActionResult Deletework_group(int id)
        {
            work_group work_group = db.work_group.Find(id);
            if (work_group == null)
            {
                return NotFound();
            }

            db.work_group.Remove(work_group);
            db.SaveChanges();

            return Ok(work_group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool work_groupExists(int id)
        {
            return db.work_group.Count(e => e.id == id) > 0;
        }
    }
}