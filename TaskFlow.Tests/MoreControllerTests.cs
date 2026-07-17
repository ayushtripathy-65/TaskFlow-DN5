// Tests for ProjectsController
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TaskFlow.API.Controllers;
using TaskFlow.API.Data;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using AutoMapper;

namespace TaskFlow.Tests;

[TestFixture]
public class ProjectsControllerTests
{
    private TaskFlowDbContext _context;
    private ProjectsController _controller;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TaskFlowDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TaskFlowDbContext(options);
        
        // Seed
        _context.Users.Add(new User { Id = 1, Name = "Test", Email = "test@test.com", PasswordHash = "test", Role = "Manager" });
        _context.Projects.Add(new Project { Id = 1, Name = "Test Project", Description = "Desc", UserId = 1 });
        _context.SaveChanges();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Project, ProjectDto>();
            cfg.CreateMap<CreateProjectDto, Project>();
            cfg.CreateMap<UpdateProjectDto, Project>();
        });
        _mapper = config.CreateMapper();

        _controller = new ProjectsController(_context, _mapper);
    }

    [TearDown]
    public void TearDown() => _context.Dispose();

    [Test]
    public async Task GetProjects_ReturnsOkResult()
    {
        var result = await _controller.GetProjects();
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetProject_ExistingId_ReturnsProject()
    {
        var result = await _controller.GetProject(1);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task CreateProject_ReturnsCreatedAtAction()
    {
        var dto = new CreateProjectDto { Name = "New Project", Description = "New Desc", UserId = 1 };
        var result = await _controller.CreateProject(dto);
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }
}

[TestFixture]
public class TaskItemsControllerTests
{
    private TaskFlowDbContext _context;
    private TaskItemsController _controller;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TaskFlowDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TaskFlowDbContext(options);
        
        _context.Users.Add(new User { Id = 1, Name = "Test", Email = "test@test.com", PasswordHash = "test", Role = "Member" });
        _context.Projects.Add(new Project { Id = 1, Name = "Project", Description = "Desc", UserId = 1 });
        _context.TaskItems.Add(new TaskItem { Id = 1, Title = "Task 1", Status = "Pending", Priority = "Medium", ProjectId = 1 });
        _context.SaveChanges();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TaskItem, TaskItemDto>();
            cfg.CreateMap<CreateTaskItemDto, TaskItem>();
            cfg.CreateMap<UpdateTaskItemDto, TaskItem>();
        });
        _mapper = config.CreateMapper();

        _controller = new TaskItemsController(_context, _mapper);
    }

    [TearDown]
    public void TearDown() => _context.Dispose();

    [Test]
    public async Task GetTaskItems_ReturnsOkResult()
    {
        var result = await _controller.GetTaskItems();
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task CreateTaskItem_ReturnsCreatedAtAction()
    {
        var dto = new CreateTaskItemDto { Title = "New Task", Description = "New", Status = "Pending", Priority = "Low", ProjectId = 1 };
        var result = await _controller.CreateTaskItem(dto);
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }
}
