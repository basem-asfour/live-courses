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
    public class SearchController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();
        [HttpGet]
        public IEnumerable<search_all_Result> all(string name)
        {
            return db.search_all(name).ToList();
        }
        [HttpGet]
        public IEnumerable<search_courses_Result> course(string name)
        {
            return db.search_courses(name).ToList();
        }
        [HttpGet]
        public IEnumerable<search_work_groups_Result> workgroup(string name)
        {
            return db.search_work_groups(name).ToList();
        }
        [HttpGet]
        public IEnumerable<search_courses_WGroup_Result> CourseGroup(string name)
        {
            return db.search_courses_WGroup(name).ToList();
        }
        [HttpGet]
        public IEnumerable<search_users_Result> user(string name)
        {
            return db.search_users(name).ToList();
        }
        [HttpGet]
        public IEnumerable<search_tags_Result> tag(string name)
        {
            return db.search_tags(name).ToList();
        }

    }
}