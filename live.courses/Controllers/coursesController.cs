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
    public class coursesController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();
        public coursesController()
        {
            // Add the following code
            // problem will be solved
            db.Configuration.ProxyCreationEnabled = false;
        }
        //finish course for only particular memper
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult finishCourseForOne(int Course_id,string memper_id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.finish_course_for_one_member(memper_id,Course_id);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseExists(Course_id))
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
        //finish course for all this course mempers
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult finishCourseForAll(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.finish_course_for_all_members(id);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseExists(id))
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

        //get all Not finished courses for A user
        public IEnumerable<get_all_not_finished_courses_for_user_Result> GetNotFinishedCourses(string id)
        {
            return db.get_all_not_finished_courses_for_user(id).ToList();
        }

        //get all finished courses for A user
        public IEnumerable<get_all_finished_courses_for_user_Result> GetFinishedCourses(string id)
        {
            return db.get_all_finished_courses_for_user(id).ToList();
        }
        //un Archive A particular course
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UnArchiveCourse(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.un_archive_course(id);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseExists(id))
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
        //Archive A particular course
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult ArchiveCourse(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.archive_course(id);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseExists(id))
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
        //get all archived courses for a user
        public IEnumerable<get_all_archived_courses_for_user_Result> GetarchivedcoursesForUser(string id)
        {
            return db.get_all_archived_courses_for_user(id).ToList();
        }
        //get all un archived courses for a user
        public IEnumerable<get_all_un_archived_courses_for_user_Result> GetUnarchivedcoursesForUser(string id)
        {
            return db.get_all_un_archived_courses_for_user(id).ToList();
        }
        //get all unArchived courses
        public IEnumerable<Get_all_unarchived_courses_Result> GetAllUnarchivedCourses()
        {
            return  db.Get_all_unarchived_courses().ToList();
        }
        //get all archived courses
        public IEnumerable<Get_all_archived_courses_Result> GetAllArchivedCourses()
        {
            return db.Get_all_archived_courses().ToList();
        }
        //get all tags for course
        public IEnumerable<get_all_course_tags_Result> Gettags(int id)
        {
            return db.get_all_course_tags(id).ToList();
        }
        // GET: api/courses
        public IQueryable<course> Getcourses()
        {
            return db.courses;
        }

        // GET: api/courses/5
        [ResponseType(typeof(course))]
        public IHttpActionResult Getcourse(int id)
        {
            course course = db.courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcourse(int id, course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.id)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseExists(id))
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

        // POST: api/courses
        [ResponseType(typeof(course))]
        public IHttpActionResult Postcourse(course course)
        {
           // course.instructor=
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Course_Members cm = new Course_Members();
            course_tags ct = new course_tags();
            ct.course_id = course.id;
            //ct.tag_id=
            cm.Course_id = course.id;
            cm.Member_id = course.instructor;
            cm.AddingDate = course.creating_date;
            db.courses.Add(course);
            db.Course_Members.Add(cm);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.id }, course);
        }

        // DELETE: api/courses/5
        [ResponseType(typeof(course))]
        public IHttpActionResult Deletecourse(int id)
        {
            course course = db.courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            db.courses.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool courseExists(int id)
        {
            return db.courses.Count(e => e.id == id) > 0;
        }
    }
}