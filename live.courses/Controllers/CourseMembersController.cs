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
    public class CourseMembersController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();

        // GET: api/CourseMembers
        public IQueryable<Course_Members> GetCourse_Members()
        {
            return db.Course_Members;
        }

        // GET: api/CourseMembers/5
        [ResponseType(typeof(Course_Members))]
        public IHttpActionResult GetCourse_Members(int id)
        {
            Course_Members course_Members = db.Course_Members.Find(id);
            if (course_Members == null)
            {
                return NotFound();
            }

            return Ok(course_Members);
        }

        // PUT: api/CourseMembers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse_Members(int id, Course_Members course_Members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course_Members.Course_id)
            {
                return BadRequest();
            }

            db.Entry(course_Members).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Course_MembersExists(id))
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

        // POST: api/CourseMembers
        [ResponseType(typeof(Course_Members))]
        public IHttpActionResult PostCourse_Members(Course_Members course_Members)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Course_Members.Add(course_Members);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Course_MembersExists(course_Members.Course_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = course_Members.Course_id }, course_Members);
        }

        // DELETE: api/CourseMembers/5
        [ResponseType(typeof(Course_Members))]
        public IHttpActionResult DeleteCourse_Members(int course_id, string member_id)
        {
            Course_Members course_Members = db.Course_Members.Find(course_id,member_id);
            if (course_Members == null)
            {
                return NotFound();
            }

            db.Course_Members.Remove(course_Members);
            db.SaveChanges();

            return Ok(course_Members);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Course_MembersExists(int id)
        {
            return db.Course_Members.Count(e => e.Course_id == id) > 0;
        }
    }
}