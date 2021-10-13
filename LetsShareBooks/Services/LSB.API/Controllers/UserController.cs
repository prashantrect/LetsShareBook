using LSB.Models;
using LSB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSB.API.Controllers
{

    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // GET api/<UserController>/5
        [HttpGet, Route("api/users/{id}")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            User userDetail = await _userRepository.GetUserAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        // POST api/<UserController>
        [HttpPost, Route("api/users")]
        public async Task<IActionResult> PostUserAsync([FromBody] User user)
        {
            User userDetail = await _userRepository.CreateUserAsync(user);
            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        // PUT api/<UserController>/5
        [HttpPut, Route("api/users/{id}")]
        public async Task<IActionResult> PutUserAsync(string id, [FromBody] User user)
        {
            var userDetail = await _userRepository.UpdateUserAsync(id, user);
            if (userDetail == null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        // DELETE api/<HomeController>/5
        [HttpDelete, Route("api/users/{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            await _userRepository.DeleteUserAsync(id);
            return Ok();
        }
    }
}


