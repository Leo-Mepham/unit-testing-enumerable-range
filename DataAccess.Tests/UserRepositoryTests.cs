using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace DataAccess.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void UserRepositoryGetsById()
        {
            // Arrange
            var users = GetTestUsers();
            var userContext = Substitute.For<UserContext>();
            userContext.Users.Returns(users);
            var userRepository = new UserRepository(userContext);

            // Act
            var user = userRepository.GetById(1);

            // Assert
            user.Id.Should().Be(1);
            user.UserId.Should().Be(new Guid(1, 0, 0, new byte[8]));
        }

        [Fact]
        public void UserRepositoryGetsByUserId()
        {
            // Arrange
            var users = GetTestUsers();
            var userContext = Substitute.For<UserContext>();
            userContext.Users.Returns(users);
            var userRepository = new UserRepository(userContext);

            // Act
            var user = userRepository.GetByUserId(new Guid(1, 0, 0, new byte[8]));

            // Assert
            user?.Id.Should().Be(1);
            user?.UserId.Should().Be(new Guid(1, 0, 0, new byte[8]));
        }

        [Fact]
        public void UserRepositoryCountsUsers()
        {
            // Arrange
            var users = GetTestUsers();
            var userContext = Substitute.For<UserContext>();
            userContext.Users.Returns(users);
            var userRepository = new UserRepository(userContext);

            // Act
            var count = userRepository.GetUserCount();

            // Assert
            count.Should().Be(100);
        }

        private DbSet<User> GetTestUsers()
        {
            var users = Enumerable
                .Range(1, 100)
                .Select(id => new User
                {
                    Id = id,
                    UserId = new Guid(id, 0, 0, new byte[8])
                })
                .ToList()
                .AsQueryable();

            // DB Set mocking from https://docs.microsoft.com/en-gb/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN#queryTest
            // If you're using Async DB Context methods (And you should be) there's a longer Async version in the link above

            var dbSet = Substitute.For<DbSet<User>, IQueryable<User>>();

            dbSet.As<IQueryable<User>>().Provider.Returns(users.Provider);
            dbSet.As<IQueryable<User>>().Expression.Returns(users.Expression);
            dbSet.As<IQueryable<User>>().ElementType.Returns(users.ElementType);
            dbSet.As<IQueryable<User>>().GetEnumerator().Returns(users.GetEnumerator());

            return dbSet;
        }
    }
}
