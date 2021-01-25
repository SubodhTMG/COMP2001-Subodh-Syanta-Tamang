using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2001.Models;

namespace COMP2001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataAccess Database;
        public UserController(DataAccess context)

        {
            Database = context;
        }


        [HttpGet]
        public IActionResult UpdateUser(User ValidateUser)
        {
            getValidation(ValidateUser);
            if (getValidation(ValidateUser) == true)
            {
                return StatusCode(200, getValidation(ValidateUser));
            }
            return StatusCode(404, getValidation(ValidateUser));
        }

        public bool getValidation(User ValidateUser)
        {

            Database.Validate(ValidateUser);

            if (Database.Validate(ValidateUser) == true)
            {
                return true;
            }

            return false;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult UpdateCurrentUser(int id, User user)
        {
            Database.Update(id, user);

            return StatusCode(204, id);
        }

        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult Register(User user)
        {
            string Message = "";

            register(user, Message);

            return StatusCode(200, Message);
        }

        private void register (User NewUser, string Message)
        {
            Database.Register(NewUser, Message);
        }
        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            Database.Delete(id);

            return StatusCode(204, id);

        }

      
    }
}
