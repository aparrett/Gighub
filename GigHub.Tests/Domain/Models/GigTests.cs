using FluentAssertions;
using GigHub.Core.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Linq;

namespace GigHub.Tests.Domain.Models
{
    [TestFixture]
    public class GigTests
    {
        [Test]
        public void Cancel_WhenCalled_GigShouldBeCancelled()
        {
            var gig = new Gig();
            gig.Cancel();

            gig.IsCancelled.Should().BeTrue();
        }

        [Test]
        public void Cancel_WhenCalled_AttendeesShouldHaveNotification()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });
            gig.Cancel();

            var attendees = gig.Attendances.Select(a => a.Attendee).ToList();
            attendees[0].UserNotifications.Should().HaveCount(1);
        }
    }
}
