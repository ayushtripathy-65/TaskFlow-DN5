using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using AutoMapper;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly TaskFlowDbContext _context;
    private readonly IMapper _mapper;

    public TaskItemsController(TaskFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTaskItems()
    {
        var tasks = await _context.TaskItems
            .Include(t => t.Project)
            .ToListAsync();
        return Ok(_mapper.Map<List<TaskItemDto>>(tasks));
    }

    [HttpGet("'{id}'")]
    public async Task<ActionResult<TaskItemDto>> GetTaskItem(int id)
    {
        var task = await _context.TaskItems
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();
        return Ok(_mapper.Map<TaskItemDto>(task));
    }

    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> CreateTaskItem(CreateTaskItemDto dto)
    {
        var task = _mapper.Map<TaskItem>(dto);
        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTaskItem), new { id = task.Id }, _mapper.Map<TaskItemDto>(task));
    }

    [HttpPut("'{id}'")]
    public async Task<IActionResult> UpdateTaskItem(int id, UpdateTaskItemDto dto)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return NotFound();
        _mapper.Map(dto, task);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("'{id}'")]
    public async Task<IActionResult> DeleteTaskItem(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return NotFound();
        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
