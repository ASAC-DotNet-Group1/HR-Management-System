//using HR_Management_System.Models.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace HR_Management_System.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NameController : ControllerBase
//    {
//        public IJwtAuthenticationManager jwtAuthenticationManager { get; private set; }

//        public NameController(IJwtAuthenticationManager jwtAuthenticationManager)
//        {
//            this.jwtAuthenticationManager =jwtAuthenticationManager;
//        }
//        // GET: api/<NameController>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { " Amman ", " Irbid " };
//        }

//        // GET api/<NameController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        [AllowAnonymous]
//        [HttpPost("Authenticate")]
//        public IActionResult Authenticate([FromBody] UserCred userCred) {

//            var token = jwtAuthenticationManager.Authenticate(userCred.UserName, userCred.Password);

//            if (token == null)
//                return Unauthorized();
//            return Ok(token);
//        }
//    }

//    public class UserCred
//    {
//        public string UserName { get; set; }
//        public string Password { get; set; }
//    }
//}
