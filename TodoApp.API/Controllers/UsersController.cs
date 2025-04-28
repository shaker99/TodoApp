using __TodoApp.Domain.Entities;
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
    public class UsersController : ControllerBase
    {
        private readonly IDataHelper<User> _dataHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IDataHelper<User> dataHelper, IMapper mapper, ILogger<UsersController> logger)
        {
            _dataHelper = dataHelper;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _dataHelper.AddAsync(user);

            _logger.LogInformation($"User created successfully with ID: {user.Id}");

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var allUsers = await _dataHelper.GetAllAsync();

            var pagedUsers = allUsers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            _logger.LogInformation($"Fetched {pagedUsers.Count} users. Page {pageNumber}.");

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Users = pagedUsers
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var user = await _dataHelper.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Fetched user with ID {id}.");
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            var existingUser = await _dataHelper.GetByIdAsync(id);
            if (existingUser == null)
            {
                _logger.LogWarning($"Attempt to update non-existing user with ID {id}.");
                return NotFound();
            }

            _mapper.Map(userDto, existingUser);
            await _dataHelper.UpdateAsync(existingUser);

            _logger.LogInformation($"User with ID {id} updated successfully.");
            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _dataHelper.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"Attempt to delete non-existing user with ID {id}.");
                return NotFound();
            }

            await _dataHelper.DeleteAsync(user.Id);
            _logger.LogInformation($"User with ID {id} deleted successfully.");
            return NoContent();
        }
    }
}
