using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace Exmart_Backend_Test_Case
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private readonly ApplicationDBContext _dbC;
        private readonly UserRepository _userRepository;
        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbC = new ApplicationDBContext(options);
            _userRepository = new UserRepository(_dbC);
            SeedData();
        }

        private void SeedData()
        {

            _dbC.Users.AddRange(
            new User { Id = 111, Name = "Robert Brown", Email = "robert.brown@example.com", Phone = "+91 9998887766", CreatedAt = DateTime.UtcNow },
            new User { Id = 222, Name = "Emily White", Email = "emily.white@example.com", Phone = "+91 9876543210", CreatedAt = DateTime.UtcNow },
            new User { Id = 333, Name = "Alice Brown", Email = "alicebrown@example.com", Phone = "1122334455", CreatedAt = DateTime.UtcNow }
        );

            _dbC.SaveChanges();

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _dbC.Database.EnsureDeleted();
            _dbC.Dispose();
        }
        [Test]
        public async Task AddUserShouldAddUserWhenValidUserProvided()
        {
            var newUser = new User
            {
                Name = "John Wick",
                Email = "jhonwick@example.com",
                Phone = "+91 9873876210",
                CreatedAt = DateTime.UtcNow
            };

            // Act
            var result = await _userRepository.AddUser(newUser);

            // Assert
            Assert.IsTrue(result);
            var addedUser = await _dbC.Users.FindAsync(newUser.Id);
            Assert.IsNotNull(addedUser);
            Assert.AreEqual("John Wick", addedUser.Name);
        }

        [Test]
        public async Task GetUserByIdShouldReturnUserWhenValidIdProvided()
        {
            // Arrange
            var userId = 333;

            // Act
            var user = await _dbC.Users.FindAsync(userId);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.Id);
            Assert.AreEqual("Alice Brown", user.Name);
        }

        [Test]
        public async Task EditUserShouldUpdateUserWhenValidUserIdProvided()
        {
            // Arrange
            var existingUser = await _dbC.Users.FindAsync(222);

            existingUser.Name = "Robert Brown Updated";

            // Act
            _dbC.Users.Update(existingUser);
            await _dbC.SaveChangesAsync();

            // Assert
            var updatedUser = await _dbC.Users.FindAsync(222);
            Assert.AreEqual("Robert Brown Updated", updatedUser.Name);
        }

        [Test]
        public async Task DeleteUserShouldDeleteUserWhenValidIdProvided()
        {
            // Arrange
            var userId = 111;
            var userToDelete = await _dbC.Users.FindAsync(userId);

            // Act
            _dbC.Users.Remove(userToDelete);
            await _dbC.SaveChangesAsync();

            // Assert
            var deletedUser = await _dbC.Users.FindAsync(userId);
            Assert.IsNull(deletedUser);
        }

        [Test]
        public async Task IsUserExistedShouldReturnTrueWhenUserExists()
        {
            // Arrange
            var userId = 333;

            // Act
            var result = await _userRepository.IsUserExisted(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsUserExistedShouldReturnFalseWhenUserDoesNotExist()
        {
            // Arrange
            var userId = 999; // Non-existing user ID

            // Act
            var result = await _userRepository.IsUserExisted(userId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ReturnIdByEmailShouldReturnUserIdWhenEmailExists()
        {
            // Arrange
            var email = "alicebrown@example.com";

            // Act
            var userId = await _userRepository.ReturnIdbyEmail(email);

            // Assert
            Assert.AreEqual(333, userId);
        }
    }
}
