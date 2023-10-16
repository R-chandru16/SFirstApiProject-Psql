using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFirstApplicationApi.Models
{
    public class User
    {
        public string UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
