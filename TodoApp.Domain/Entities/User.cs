using __TodoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace __TodoApp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; }

        public Role Role { get; set; } = Role.Guest;

        public ICollection<TaskItem>? Tasks { get; set; } 

    }
}
