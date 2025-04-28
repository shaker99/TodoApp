using __TodoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTO
{
    public class TaskDto
    {

        public string? Title { get; set; }
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public TaskPriority Priority { get; set; }
        public TaskCategory Category { get; set; }

        public int UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
