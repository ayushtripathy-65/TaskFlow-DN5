using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using AutoMapper;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  // ADD THIS
public class UsersController : ControllerBase
{
    private readonly TaskFlowDbContext _context;
    private readonly IMapper _mapper;

    public UsersController(TaskFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]  // Role-based
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _context.Users
            .Include(u => u.Projects)
            .ToListAsync();
        return Ok(_mapper.Map<List<UserDto>>(users));
    }

    [HttpGet("'{id}'")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost]
    [AllowAnonymous]  // Anyone can register
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = dto.Password;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserDto>(user));
    }

    [HttpPut("'{id}'")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        _mapper.Map(dto, user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("'{id}'")]
    [Authorize(Roles = "Admin")]  // Only Admin can delete
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
