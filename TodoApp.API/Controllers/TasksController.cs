using __TodoApp.Domain.Entities;
using __TodoApp.Domain.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApp.Application.DTO;
using TodoApp.Application.Interfaces;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class TasksController : ControllerBase
    {
        private readonly IDataHelper<TaskItem> _dataHelper;
        private readonly IMapper mapper;
        private readonly ILogger<TasksController> _logger;

        public TasksController(IDataHelper<TaskItem> dataHelper, IMapper mapper, ILogger<TasksController> logger)
        {
            _dataHelper = dataHelper;
            this.mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskDto taskDto)
        {
            var task = mapper.Map<TaskItem>(taskDto);
            await _dataHelper.AddAsync(task);

            _logger.LogInformation($"Task created successfully with ID: {task.Id}");

            return Ok(task);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(
            TaskPriority? priority = null,
            TaskCategory? category = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var allTasks = await _dataHelper.GetAllAsync();

            if (priority.HasValue)
            {
                allTasks = allTasks.Where(t => t.Priority == priority.Value).ToList();
                _logger.LogInformation($"Filtered tasks by priority: {priority.Value}");
            }

            if (category.HasValue)
            {
                allTasks = allTasks.Where(t => t.Category == category.Value).ToList();
                _logger.LogInformation($"Filtered tasks by category: {category.Value}");
            }

            var pagedTasks = allTasks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            _logger.LogInformation($"Fetched {pagedTasks.Count} tasks. Page {pageNumber}.");

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = pagedTasks
            });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var taskItem = await _dataHelper.GetByIdAsync(id);
            if (taskItem == null)
            {
                _logger.LogWarning($"Task with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Fetched task with ID {id}.");
            return Ok(taskItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskDto taskDto)
        {
            var existingTask = await _dataHelper.GetByIdAsync(id);
            if (existingTask == null)
            {
                _logger.LogWarning($"Attempt to update non-existing task with ID {id}.");
                return NotFound();
            }

            mapper.Map(taskDto, existingTask);
            await _dataHelper.UpdateAsync(existingTask);

            _logger.LogInformation($"Task with ID {id} updated successfully.");

            return Ok(existingTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskItem = await _dataHelper.GetByIdAsync(id);
            if (taskItem == null)
            {
                _logger.LogWarning($"Attempt to delete non-existing task with ID {id}.");
                return NotFound();
            }

            await _dataHelper.DeleteAsync(taskItem.Id);

            _logger.LogInformation($"Task with ID {id} deleted successfully.");

            return NoContent();
        }
    }
}
