using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Models.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string userName, string Password);
    }
}
