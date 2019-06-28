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
    public class FriendsController : ApiController
    {
        private adv_coursesEntities db = new adv_coursesEntities();
        //Remove friend
        [HttpDelete]
        [ResponseType(typeof(friend))]
        public IHttpActionResult Deletefriend(string User1,string User2)
        {
            friend friend1 = db.friends.Find(User1,User2);
            friend friend2 = db.friends.Find(User2, User1);
            if (friend1 == null&&friend2==null)
            {
                return NotFound();
            }

     //       db.friends.Remove(friend);

            db.remove_friend_or_frequest(User1, User2);
            db.SaveChanges();
            if (friend1==null)
	        {
		        return Ok(friend2);
	        }
            else
	        {
                return Ok(friend1);
	        }
        }
       //  POST send friend Request
        [HttpPost]
        [ResponseType(typeof(friend))]
        public IHttpActionResult SendRequest(friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.send_friend_request(friend.UserId, friend.friendId, friend.since);
            //db.friends.Add(friend);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (friendExists(friend.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = friend.UserId }, friend);
        }
        //Accept friend Request 
        [HttpPut]
        public IHttpActionResult AcceptRequest(string Sender,string Reciver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.accept_friend_request(Reciver, Sender);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!friendExists(Sender))
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

        //get All friends for a user
        public IEnumerable<get_all_friends_Result> GetFriends(string id)
        {
            return db.get_all_friends(id).ToList();
        }
        //get All friend Request for a user
        public IEnumerable<get_all_friend_requests_Result> GetFriendRequests(string id)
        {
            return db.get_all_friend_requests(id).ToList();
        }
        // GET api/Friends
        //public IQueryable<friend> Getfriends()
        //{
        //    return db.friends;
        //}

        // GET api/Friends/5
        [ResponseType(typeof(friend))]
        public IHttpActionResult Getfriend(string id)
        {
            friend friend = db.friends.Find(id);
            if (friend == null)
            {
                return NotFound();
            }

            return Ok(friend);
        }

        // PUT api/Friends/5
        public IHttpActionResult Putfriend(string id, friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friend.UserId)
            {
                return BadRequest();
            }

            db.Entry(friend).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!friendExists(id))
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

        // POST api/Friends
        //[ResponseType(typeof(friend))]
        //public IHttpActionResult Postfriend(friend friend)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.friends.Add(friend);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (friendExists(friend.UserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = friend.UserId }, friend);
        //}

        // DELETE api/Friends/5
        //[ResponseType(typeof(friend))]
        //public IHttpActionResult Deletefriend(string id)
        //{
        //    friend friend = db.friends.Find(id);
        //    if (friend == null)
        //    {
        //        return NotFound();
        //    }

        //    db.friends.Remove(friend);
        //    db.SaveChanges();

        //    return Ok(friend);
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool friendExists(string id)
        {
            return db.friends.Count(e => e.UserId == id) > 0||db.friends.Count(f=>f.friendId==id)>0;
        }
    }
}