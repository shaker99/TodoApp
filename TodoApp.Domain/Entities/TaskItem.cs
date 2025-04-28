﻿using __TodoApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace __TodoApp.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string? Title { get; set; } 
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; }
        

        public TaskPriority Priority { get; set; } 
        public TaskCategory Category { get; set; } 
        
        public int UserId { get; set; }
        public User? User { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
