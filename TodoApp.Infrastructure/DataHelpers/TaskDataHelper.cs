using __TodoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.DataHelpers
{
    public class TaskDataHelper : IDataHelper<TaskItem>
    {
        private readonly AppDbContext _context;

        public TaskDataHelper(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskItem entity)
        {
            _context.TodoTasks.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);
            if (task != null)
            {
                _context.TodoTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.TodoTasks.Include(t => t.User).ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.TodoTasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(TaskItem entity)
        {
            _context.TodoTasks.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
