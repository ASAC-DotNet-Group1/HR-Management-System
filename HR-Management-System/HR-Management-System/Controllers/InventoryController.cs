//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace HR_Management_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class InventoryController : ControllerBase
//    {
//        // GET: api/<InventoryController>
//        [Authorize(Roles = "Admin , User")]
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // POST api/<InventoryController>
//        [Authorize(Roles = "Admin")]
//        [HttpPost]
//        public void Post([FromBody] InventoryController value)
//        {
//        }

//    }
//}
