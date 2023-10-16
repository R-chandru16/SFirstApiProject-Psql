using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SFirstApplicationApi.Models;
using SFirstApplicationApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SFirstApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;


        public UserController(UserService usersevice)  //constructor
        {
            _service = usersevice;
        }



        // POST api/<UserController>
        [HttpPost]    // here 1 post. url shows as api/user
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserDTO user)
        {
            var userDTO = _service.Register(user);

            if (userDTO != null)
                return userDTO;
            return BadRequest("Not able to register");

        }

        // PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] User user)
        //{

        //}




        [Route("Login")]  //(so to avoid confusion/ambiquity here we saying add "login" to the url) here url shows as  api/user/login  this is called attribute based routing.
        [HttpPost]    //here 2nd post 
        public async Task<ActionResult<UserDTO>> Put([FromBody] UserDTO user)
        {

            var userDTO = _service.Login(user);
            if (userDTO != null)
                return Ok(userDTO);
            return BadRequest("Not able to register");
        }


        // DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //// DELETE api/<UserController>
      
        //public IActionResult Delete([FromBody] UserDTO userDto)
        //{
        //    try
        //    {
        //        bool deleted = _service.DeleteUser(userDto);

        //        if (deleted)
        //        {
        //            return NoContent(); // Return a 204 No Content response if the user is successfully deleted.
        //        }

        //        return NotFound(); // Return a 404 Not Found response if the user with the given ID is not found.
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return StatusCode(500, $"Internal Server Error: {e.Message}");
        //    }
        //}


    }
}
