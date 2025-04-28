using __TodoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTO
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; } 

        public Role Role { get; set; } 
    }
}
