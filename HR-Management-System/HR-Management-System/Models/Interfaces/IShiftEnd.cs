using HR_Management_System.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IShiftEnd
    {
        public Task<ShiftEnd> GetShiftEnd(int id);

        public Task<List<ShiftEndDTO>> GetShiftEnds();

        public Task AddShiftEnd(ShiftEndDTO shiftEnd);

        public Task UpdateShiftEnd(int id, ShiftEnd shiftEnd);

        public Task DeleteShiftEnd(int id);
    }
}
