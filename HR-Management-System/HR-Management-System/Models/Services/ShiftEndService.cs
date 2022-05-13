using HR_Management_System.Data;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Services
{
    public class ShiftEndService : IShiftEnd
    {
        private readonly HR_DbContext _context;

        public ShiftEndService(HR_DbContext context)
        {
            _context = context;
        }

        public async Task AddShiftEnd(ShiftEndDTO ShiftEnddto)
        {
            ShiftEnd ShiftEnd = new ShiftEnd
            {
                EmployeeID = ShiftEnddto.EmployeeID,
                Left = ShiftEnddto.Left,
                Date = System.DateTime.Now.ToLocalTime()
            };
            _context.Entry(ShiftEnd).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteShiftEnd(int id)
        {
            ShiftEnd ShiftEnd = await GetShiftEnd(id);

            _context.Entry(ShiftEnd).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<ShiftEnd> GetShiftEnd(int id)
        {
            return await _context.ShiftEnds.FindAsync(id);
        }

        /// <summary>
        /// Return all ShiftEnds
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShiftEndDTO>> GetShiftEnds()
        {

            return await _context.ShiftEnds.Select(x => new ShiftEndDTO()
            {
                EmployeeID = x.EmployeeID,
                Date = x.Date,
                Left = x.Left
            }).ToListAsync();

        }

        public async Task UpdateShiftEnd(int id, ShiftEnd ShiftEnd)
        {
            _context.Entry(ShiftEnd).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

    }

}
