using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Moq;
using NUnit.Framework;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestFixture]
    public class UserNotificationRepositoryTests
    {
        private UserNotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;

        [SetUp]
        public void SetUp()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);

            _repository = new UserNotificationRepository(mockContext.Object);
        }

        [Test]
        [Ignore("Incomplete")]
        public void GetUnread()
        {
        }
    }
}
