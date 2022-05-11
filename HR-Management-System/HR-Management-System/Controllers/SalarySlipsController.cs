//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using HR_Management_System.Data;
//using HR_Management_System.Models;

//namespace HR_Management_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SalarySlipsController : ControllerBase
//    {
//        private readonly HR_DbContext _context;

//        public SalarySlipsController(HR_DbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/SalarySlips
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<SalarySlip>>> GetSalarySlips()
//        {
//            return await _context.SalarySlips.ToListAsync();
//        }

//        // GET: api/SalarySlips/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<SalarySlip>> GetSalarySlip(int id)
//        {
//            var salarySlip = await _context.SalarySlips.FindAsync(id);

//            if (salarySlip == null)
//            {
//                return NotFound();
//            }

//            return salarySlip;
//        }

//        // PUT: api/SalarySlips/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> PutSalarySlip(int id, SalarySlip salarySlip)
//        //{
//        //    if (id != salarySlip.ID)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    _context.Entry(salarySlip).State = EntityState.Modified;

//        //    try
//        //    {
//        //        await _context.SaveChangesAsync();
//        //    }
//        //    catch (DbUpdateConcurrencyException)
//        //    {
//        //        if (!SalarySlipExists(id))
//        //        {
//        //            return NotFound();
//        //        }
//        //        else
//        //        {
//        //            throw;
//        //        }
//        //    }

//        //    return NoContent();
//        //}

//        // POST: api/SalarySlips
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<SalarySlip>> PostSalarySlip(SalarySlip salarySlip)
//        {
//            _context.SalarySlips.Add(salarySlip);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetSalarySlip", new { id = salarySlip.ID }, salarySlip);
//        }

//        // DELETE: api/SalarySlips/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteSalarySlip(int id)
//        {
//            var salarySlip = await _context.SalarySlips.FindAsync(id);
//            if (salarySlip == null)
//            {
//                return NotFound();
//            }

//            _context.SalarySlips.Remove(salarySlip);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        //private bool SalarySlipExists(int id)
//        //{
//        //    return _context.SalarySlips.Any(e => e.ID == id);
//        //}
//    }
//}
