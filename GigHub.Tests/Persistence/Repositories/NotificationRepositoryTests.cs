using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestFixture]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;

        [SetUp]
        public void SetUp()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }

        [Test]
        public void GetUnreadNotifications_NotificationIsRead_ShouldNotBeReturned()
        {
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, Notification.GigCreated(new Gig()));
            userNotification.Read();

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetUnreadNotifications(user.Id);

            notifications.Should().BeEmpty();
        }

        [Test]
        public void GetUnreadNotifications_NotificationForDifferentUser_ShouldNotBeReturned()
        {
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, Notification.GigCreated(new Gig()));

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetUnreadNotifications("2");

            notifications.Should().BeEmpty();
        }

        [Test]
        public void GetUnreadNotifications_NotificationBelongsToUserAndIsUnread_ShouldBeReturned()
        {
            var notification = Notification.GigCreated(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetUnreadNotifications(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().BeOfType<Notification>();
        }
    }
}
