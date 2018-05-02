using GigHub.Controllers.api;
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GigHub.Tests.Controllers.Api
{
    [TestFixture]
    public class NotificationsControllerTests
    {
        private Mock<INotificationRepository> _mockRepository;
        private NotificationsController _controller;
        private string _userId;
        private Mock<IUserNotificationRepository> _mockUserNotificationRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<INotificationRepository>();
            _mockUserNotificationRepository = new Mock<IUserNotificationRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Notifications).Returns(_mockRepository.Object);
            mockUnitOfWork.SetupGet(u => u.UserNotifications).Returns(_mockUserNotificationRepository.Object);

            _controller = new NotificationsController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }
    }
}
