// NUnit Hands-on: Unit tests for TaskFlow controllers
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Controllers;
using TaskFlow.API.Data;
using TaskFlow.Core.DTOs;
using TaskFlow.Core.Entities;
using AutoMapper;

namespace TaskFlow.Tests;

[TestFixture]
public class UsersControllerTests
{
    private TaskFlowDbContext _context;
    private UsersController _controller;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TaskFlowDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TaskFlowDbContext(options);

        // Seed data
        _context.Users.Add(new User { Id = 1, Name = "Test User", Email = "test@test.com", PasswordHash = "test123", Role = "Member" });
        _context.SaveChanges();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<CreateUserDto, User>();
            cfg.CreateMap<UpdateUserDto, User>();
        });
        _mapper = config.CreateMapper();

        _controller = new UsersController(_context, _mapper);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task GetUsers_ReturnsOkResult()
    {
        var result = await _controller.GetUsers();
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetUser_ExistingId_ReturnsUser()
    {
        var result = await _controller.GetUser(1);
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.InstanceOf<UserDto>());
    }

    [Test]
    public async Task GetUser_NonExistingId_ReturnsNotFound()
    {
        var result = await _controller.GetUser(999);
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateUser_ReturnsCreatedAtAction()
    {
        var dto = new CreateUserDto { Name = "New User", Email = "new@test.com", Password = "pass123", Role = "Member" };
        var result = await _controller.CreateUser(dto);
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
    }

    [Test]
    public async Task DeleteUser_ExistingId_ReturnsNoContent()
    {
        var result = await _controller.DeleteUser(1);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteUser_NonExistingId_ReturnsNotFound()
    {
        var result = await _controller.DeleteUser(999);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
