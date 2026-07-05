using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using AutoMapper;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly TaskFlowDbContext _context;
    private readonly IMapper _mapper;

    public ProjectsController(TaskFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var projects = await _context.Projects
            .Include(p => p.User)
            .Include(p => p.Tasks)
            .ToListAsync();
        return Ok(_mapper.Map<List<ProjectDto>>(projects));
    }

    [HttpGet("'{id}'")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id)
    {
        var project = await _context.Projects
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (project == null) return NotFound();
        return Ok(_mapper.Map<ProjectDto>(project));
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(CreateProjectDto dto)
    {
        var project = _mapper.Map<Project>(dto);
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, _mapper.Map<ProjectDto>(project));
    }

    [HttpPut("'{id}'")]
    public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto dto)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();
        _mapper.Map(dto, project);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("'{id}'")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
