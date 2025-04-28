using __TodoApp.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTO;

namespace TodoApp.Application.Mappings
{
    public class For_Mapping : Profile
    {
        public For_Mapping()
        {
            CreateMap<UserDto, User>();
            CreateMap<TaskDto, TaskItem>();

        }
    }
}
