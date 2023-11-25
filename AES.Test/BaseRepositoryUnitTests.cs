using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPS_AES.EF;
using TRPS_AES.Entities;

namespace AES.Test
{
    public class BaseRepositoryUnitTests
    {
        [Fact]
        public void Create_InputUsersInstance_CalledAddMethodOfDBSetWithUsersInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<AESContext>()
            .Options;
            var mockContext = new Mock<AESContext>(opt);
            var mockDbSet = new Mock<DbSet<Users>>();
            mockContext
            .Setup(context =>
            context.Set<Users>(
            ))
            .Returns(mockDbSet.Object);
            var repository = new TestUsersRepository(mockContext.Object);
            Users expectedStreet = new Mock<Users>().Object;
            //Act
            repository.Create(expectedStreet);
            // Assert
            mockDbSet.Verify(
            dbSet => dbSet.Add(
            expectedStreet
            ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<AESContext>()
            .Options;
            var mockContext = new Mock<AESContext>(opt);
            var mockDbSet = new Mock<DbSet<Users>>();
            mockContext
            .Setup(context =>
            context.Set<Users>(
            ))
            .Returns(mockDbSet.Object);
            Users expectedStreet = new Users() { IdUser = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedStreet.IdUser))
            .Returns(expectedStreet);
            var repository = new TestUsersRepository(mockContext.Object);
            //Act
            var actualStreet = repository.Get(expectedStreet.IdUser);
            // Assert
            mockDbSet.Verify(
            dbSet => dbSet.Find(
            expectedStreet.IdUser
            ), Times.Once());
            Assert.Equal(expectedStreet, actualStreet);
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<AESContext>().Options;
            var mockContext = new Mock<AESContext>(opt);
            var mockDbSet = new Mock<DbSet<Users>>();
            mockContext.Setup(context => context.Set<Users>()).Returns(mockDbSet.Object);

            Users expectedStreet = new Users() { IdUser = 1 };

            mockDbSet.Setup(mock => mock.Find(expectedStreet.IdUser)).Returns(expectedStreet);

            var repository = new TestUsersRepository(mockContext.Object);

            // Act
            repository.Delete(expectedStreet.IdUser);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Find(expectedStreet.IdUser), Times.Once());
            mockDbSet.Verify(dbSet => dbSet.Remove(expectedStreet), Times.Once());
        }

    }
}