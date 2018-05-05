using FluentAssertions;
using GigHub.Core.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Linq;

namespace GigHub.Tests.Domain.Models
{
    [TestFixture]
    public class ApplicationUserTests
    {
        [Test]
        public void Notify_WhenCalled_ShouldAddTheNotification()
        {
            var user = new ApplicationUser();
            var notification = Notification.GigCancelled(new Gig());

            user.Notify(notification);

            user.UserNotifications.Count.Should().Be(1);

            var userNotification = user.UserNotifications.First();
            userNotification.Notification.Should().Be(notification);
            userNotification.User.Should().Be(user);
        }
    }
}
