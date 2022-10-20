using ApiUsersAuthentication.Context;
using ApiUsersAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiUsersAuthentication.Controllers
{
    [Route("/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UsersController(AppDbContext db)
        {
            _db = db;
        }
        // GET: api/<UsersController>
        [HttpGet, Route("api/GetUsers")]
        public ActionResult Get()
        {
            return Ok(_db.users.ToList());
        }

        [HttpGet, Route("api/AuthenticateUser")]

        public ActionResult AuthenticateUser(string userName, string password)
        {
            UserAuth userAuth = new UserAuth();

            return Ok(userAuth.UserAuthentication(userName, password));
        }

        [HttpPost, Route("api/InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody] User user)
        {
            try
            {
                _ = _db.users.Add(user);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<ItemsController>/5
        [HttpPut, Route("api/UpdateUser")]
        public ActionResult UpdateUser(int id, [FromBody] User user)
        {
            try
            {

                if (user.id == id)
                {
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete, Route("api/DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var user = _db.users.FirstOrDefault(g => g.id == id);
                if (user != null)
                {
                    _db.users.Remove(user);
                    _db.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
