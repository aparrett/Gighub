using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestFixture]
    public class AttendanceRepositoryTests
    {
        private AttendanceRepository _repository;
        private Mock<DbSet<Attendance>> _mockAttendances;

        [SetUp]
        public void SetUp()
        {
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);

            _repository = new AttendanceRepository(mockContext.Object);
        }

        [Test]
        public void GetFutureAttendances_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            var attendance = new Attendance { AttendeeId = "1", Gig = gig };

            _mockAttendances.SetSource(new[] { attendance });

            var attendances = _repository.GetFutureAttendances("1");

            attendances.Should().BeEmpty();
        }

        [Test]
        public void GetFutureAttendances_GigForDifferentUser_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            var attendance = new Attendance { AttendeeId = "1", Gig = gig };

            _mockAttendances.SetSource(new[] { attendance });

            var attendances = _repository.GetFutureAttendances("2");

            attendances.Should().BeEmpty();
        }

        [Test]
        public void GetFutureAttendance_GigInFutureForGivenUser_ShouldBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            var attendance = new Attendance { AttendeeId = "1", Gig = gig };

            _mockAttendances.SetSource(new[] { attendance });

            var attendances = _repository.GetFutureAttendances("1");

            attendances.Should().Contain(attendance);
        }

        [Test]
        public void GetAttendance_WhenCalled_ShouldReturnAttendanceWithGigIdAndAttendeeId()
        {
            var attendance = new Attendance {AttendeeId = "1", GigId = 1};

            _mockAttendances.SetSource(new[] {attendance});

            var attendanceFromRepository = _repository.GetAttendance(2, "1");

            attendanceFromRepository.Should().BeNull();
        }

        [Test]
        public void GetAttendance_AttendanceForDifferentUser_ShouldNotBeReturned()
        {
            var attendance = new Attendance {AttendeeId = "1", GigId = 1};

            _mockAttendances.SetSource(new[] {attendance});

            var attendanceFromRepository = _repository.GetAttendance(1, "2");

            attendanceFromRepository.Should().BeNull();
        }

        [Test]
        public void GetAttendance_AttendanceForUserAndGig_ShouldBeReturned()
        {
            var attendance = new Attendance {AttendeeId = "1", GigId = 1};

            _mockAttendances.SetSource(new[] {attendance});

            var attendanceFromRepository = _repository.GetAttendance(1, "1");

            attendanceFromRepository.Should().BeOfType<Attendance>();
        }
    }
}
