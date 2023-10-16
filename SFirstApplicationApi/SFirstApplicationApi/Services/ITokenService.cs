using SFirstApplicationApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFirstApplicationApi.Services
{
    public interface ITokenService

    {
        public string CreateToken(UserDTO userDTO); // interface i.e this method is going to use
  
    }
}
