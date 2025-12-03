using System;
using System.Collections.Generic;
using System.Linq;
using SampleApp.API.Entities;
using SampleApp.API.Repositories;
using Xunit;

namespace SampleApp.API.Tests.Repositories
{
    public class UsersMemoryRepositoryTests : IDisposable
    {
        private readonly UsersMemoryRepository _repository;
        private readonly User _testUser;

        public UsersMemoryRepositoryTests()
        {
            _repository = new UsersMemoryRepository();
            _testUser = new User { Id = 1, Name = "Test User" };
            _repository.Users.Add(_testUser);
        }

        public void Dispose()
        {
            // Очищаем репозиторий после каждого теста
            _repository.Users.Clear();
        }

        [Fact]
        public void Constructor_InitializesEmptyUsersList()
        {
            // Arrange
            var repo = new UsersMemoryRepository();

            // Assert
            Assert.NotNull(repo.Users);
            Assert.Empty(repo.Users);
        }

        [Fact]
        public void GetUsers_ReturnsAllUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = 1, Name = "User1" },
                new User { Id = 2, Name = "User2" },
                new User { Id = 3, Name = "User3" }
            };
            
            _repository.Users.Clear();
            _repository.Users.AddRange(expectedUsers);

            // Act
            var result = _repository.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count);
            Assert.Equal(expectedUsers, result);
        }

        [Fact]
        public void GetUsers_WhenEmpty_ReturnsEmptyList()
        {
            // Arrange
            _repository.Users.Clear();

            // Act
            var result = _repository.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void FindUserById_WhenUserExists_ReturnsUser()
        {
            // Arrange
            var expectedUser = _testUser;

            // Act
            var result = _repository.FindUserById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Name, result.Name);
            Assert.Same(expectedUser, result);
        }

        [Fact]
        public void FindUserById_WhenUserDoesNotExist_ThrowsException()
        {
            // Arrange
            var nonExistentId = 999;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _repository.FindUserById(nonExistentId));
            Assert.Equal($"Нет пользователя с id = {nonExistentId}", exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void FindUserById_WhenInvalidId_ThrowsException(int invalidId)
        {
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _repository.FindUserById(invalidId));
            Assert.Equal($"Нет пользователя с id = {invalidId}", exception.Message);
        }

        [Fact]
        public void CreateUser_AddsUserToList()
        {
            // Arrange
            _repository.Users.Clear();
            var newUser = new User { Id = 10, Name = "New User" };

            // Act
            var result = _repository.CreateUser(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser, result);
            Assert.Single(_repository.Users);
            Assert.Contains(newUser, _repository.Users);
        }

        [Fact]
        public void CreateUser_ReturnsSameUser()
        {
            // Arrange
            var newUser = new User { Id = 5, Name = "John Doe" };

            // Act
            var result = _repository.CreateUser(newUser);

            // Assert
            Assert.Same(newUser, result);
            Assert.Equal(newUser.Id, result.Id);
            Assert.Equal(newUser.Name, result.Name);
        }

        [Fact]
        public void CreateUser_WithNullUser_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _repository.CreateUser(null));
        }

        [Fact]
        public void EditUser_WhenUserExists_UpdatesUserName()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "Updated Name" };

            // Act
            var result = _repository.EditUser(updatedUser, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Name, result.Name);
            Assert.Equal(1, result.Id);
            Assert.Equal("Updated Name", _testUser.Name);
        }

        [Fact]
        public void EditUser_WhenUserExists_ReturnsUpdatedUser()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "New Name" };

            // Act
            var result = _repository.EditUser(updatedUser, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_testUser, result);
            Assert.Equal("New Name", result.Name);
        }

        [Fact]
        public void EditUser_WhenUserDoesNotExist_ThrowsException()
        {
            // Arrange
            var nonExistentId = 999;
            var user = new User { Id = 999, Name = "Non Existent" };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _repository.EditUser(user, nonExistentId));
            Assert.Equal($"Нет пользователя с id = {nonExistentId}", exception.Message);
        }

        [Fact]
        public void EditUser_WithDifferentIds_ThrowsException()
        {
            // Arrange
            var user = new User { Id = 2, Name = "Different ID" };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _repository.EditUser(user, 1));
            // Ожидаем, что метод FindUserById выбросит исключение, если не найдет пользователя с id=2
        }

        [Fact]
        public void DeleteUser_WhenUserExists_RemovesUser()
        {
            // Arrange
            var initialCount = _repository.Users.Count;

            // Act
            var result = _repository.DeleteUser(1);

            // Assert
            Assert.True(result);
            Assert.Equal(initialCount - 1, _repository.Users.Count);
            Assert.DoesNotContain(_testUser, _repository.Users);
        }

        [Fact]
        public void DeleteUser_WhenUserExists_ReturnsTrue()
        {
            // Act
            var result = _repository.DeleteUser(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteUser_WhenUserDoesNotExist_ThrowsException()
        {
            // Arrange
            var nonExistentId = 999;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _repository.DeleteUser(nonExistentId));
            Assert.Equal($"Нет пользователя с id = {nonExistentId}", exception.Message);
        }

        [Fact]
        public void DeleteUser_WhenMultipleUsersExist_RemovesCorrectUser()
        {
            // Arrange
            var user2 = new User { Id = 2, Name = "User2" };
            var user3 = new User { Id = 3, Name = "User3" };
            
            _repository.Users.Add(user2);
            _repository.Users.Add(user3);

            // Act
            _repository.DeleteUser(2);

            // Assert
            Assert.Contains(_testUser, _repository.Users);
            Assert.DoesNotContain(user2, _repository.Users);
            Assert.Contains(user3, _repository.Users);
            Assert.Equal(2, _repository.Users.Count);
        }

        [Fact]
        public void UsersProperty_IsPublicAndMutable()
        {
            // Arrange & Act
            var users = _repository.Users;

            // Assert
            Assert.NotNull(users);
            Assert.IsType<List<User>>(users);
            
            // Проверяем, что свойство действительно изменяемое
            users.Add(new User { Id = 100, Name = "Test" });
            Assert.Contains(_repository.Users, u => u.Id == 100);
        }

        [Fact]
        public void IntegrationTest_FullCRUD()
        {
            // Arrange
            _repository.Users.Clear();
            
            // Create
            var user1 = _repository.CreateUser(new User { Id = 1, Name = "User1" });
            var user2 = _repository.CreateUser(new User { Id = 2, Name = "User2" });
            
            // Assert Create & Read
            Assert.Equal(2, _repository.GetUsers().Count);
            Assert.Equal("User1", _repository.FindUserById(1).Name);
            
            // Update
            var updatedUser = new User { Id = 1, Name = "Updated User1" };
            _repository.EditUser(updatedUser, 1);
            
            // Assert Update
            Assert.Equal("Updated User1", _repository.FindUserById(1).Name);
            
            // Delete
            _repository.DeleteUser(2);
            
            // Assert Delete
            Assert.Single(_repository.GetUsers());
            Assert.Contains(_repository.GetUsers(), u => u.Id == 1);
            Assert.DoesNotContain(_repository.GetUsers(), u => u.Id == 2);
        }
    }
}