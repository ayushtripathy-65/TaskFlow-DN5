// Moq Hands-on: Write Testable Code with Moq
// Demonstrates mocking dependencies for unit testing

using Moq;
using NUnit.Framework;
using TaskFlow.Core.Entities;

namespace TaskFlow.Tests;

[TestFixture]
public class MoqTests
{
    [Test]
    public void MockUserRepository_ReturnsExpectedUser()
    {
        // Arrange - Mock a repository interface
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetById(1)).Returns(new User 
        { 
            Id = 1, 
            Name = "Repo User", 
            Email = "repo@test.com", 
            PasswordHash = "repo123", 
            Role = "Member" 
        });

        // Act
        var user = mockRepo.Object.GetById(1);

        // Assert
        Assert.That(user, Is.Not.Null);
        Assert.That(user.Name, Is.EqualTo("Repo User"));
    }

    [Test]
    public void MockUserRepository_GetAll_ReturnsMultipleUsers()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<User>
        {
            new User { Id = 1, Name = "User 1", Email = "u1@test.com", PasswordHash = "pass1", Role = "Admin" },
            new User { Id = 2, Name = "User 2", Email = "u2@test.com", PasswordHash = "pass2", Role = "Member" }
        });

        // Act
        var users = mockRepo.Object.GetAll().ToList();

        // Assert
        Assert.That(users, Has.Count.EqualTo(2));
        Assert.That(users[0].Role, Is.EqualTo("Admin"));
    }

    [Test]
    public void MockUserRepository_VerifyAddCalled()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var newUser = new User 
        { 
            Id = 3, 
            Name = "New User", 
            Email = "new@test.com", 
            PasswordHash = "new123", 
            Role = "Member" 
        };

        // Act
        mockRepo.Object.Add(newUser);
        mockRepo.Object.SaveChanges();

        // Assert - Verify methods were called
        mockRepo.Verify(r => r.Add(It.Is<User>(u => u.Name == "New User")), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }
}

public interface IUserRepository
{
    User? GetById(int id);
    IEnumerable<User> GetAll();
    void Add(User user);
    void SaveChanges();
}
