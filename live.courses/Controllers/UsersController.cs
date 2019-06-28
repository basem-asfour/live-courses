using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using live.courses.Classes_for_sp;
using live.courses.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace live.courses.Controllers
{
    public class UsersController : ApiController
    {
        

        private adv_coursesEntities db = new adv_coursesEntities();
        [HttpGet]
        public IHttpActionResult Getid()
        {
           // string ss = System.Web.HttpContext.Current.User.Identity.GetUserId();
           // string id = User.Identity.GetUserId();
            
           //  string iiid = RequestContext.Principal.Identity.GetUserId();
            return Ok(System.Web.HttpContext.Current.User.Identity.GetUserId());
            // Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(iid);
            // return System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

        }
        public IHttpActionResult GetUserinfo(string name)
        {
            AspNetUser aspNetUser = db.AspNetUsers.FirstOrDefault(user=>user.UserName==name);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return Ok(aspNetUser);
        }
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSecurityInfo(string id, security_info info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != info.Id)
            {
                return BadRequest();
            }

            db.add_security_info(info.Id, info.GuaranteeDecument, info.IdCard, info.Residence, info.Photograph);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(id))
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

      //  private string Userid { get => User.Identity.GetUserId(); set => Userid = value; }
        [Authorize]
        [HttpPut]
        public IHttpActionResult PutEditUser(string id, Sp_Edit_User User)
        {//id
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != User.Id)
            {
                return BadRequest();
            }
            db.edit_user(User.Id,User.Email,User.PhoneNumber,User.UserName,User.Country,User.State,User.Gender,User.Photo,User.Apout,User.AnotherAccount);
            //db.Entry(aspNetUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(id))
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
        // GET: api/Users
        public IQueryable<AspNetUser> GetAspNetUsers()
        {
            return db.AspNetUsers;
        }

        // GET: api/Users/5
        [ResponseType(typeof(AspNetUser))]
        public IHttpActionResult GetAspNetUser(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return Ok(aspNetUser);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAspNetUser(string id, AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspNetUser.Id)
            {
                return BadRequest();
            }

            db.Entry(aspNetUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(AspNetUser))]
        public IHttpActionResult PostAspNetUser(AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AspNetUsers.Add(aspNetUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserExists(aspNetUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aspNetUser.Id }, aspNetUser);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(AspNetUser))]
        public IHttpActionResult DeleteAspNetUser(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();

            return Ok(aspNetUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetUserExists(string id)
        {
            return db.AspNetUsers.Count(e => e.Id == id) > 0;
        }
    }
}