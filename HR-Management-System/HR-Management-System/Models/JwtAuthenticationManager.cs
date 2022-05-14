using HR_Management_System.Models.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management_System.Models
{
   

    
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IList<User> users = new List<User>
        {
            new User { UserName =  "Test 1" ,Password = "Password 1" , Role = "Admin"}, 
            new User { UserName =  "Test 2" ,Password = "Password 2" , Role = "User"}
        
        };

        public JwtAuthenticationManager(string key) {
            this.Key = key;
        }

        public string Key { get; private set; }

        public string Authenticate(string userName, string Password)
        {
            if (users.Any(u => u.UserName == userName && u.Password == Password))
            {

                return null;

            }



            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                   new Claim(ClaimTypes.Name, userName)
               }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)

            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        
        }
    }
}
