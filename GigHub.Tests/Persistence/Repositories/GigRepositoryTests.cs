using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestFixture]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [SetUp]
        public void SetUp()
        {
            _mockGigs = new Mock<DbSet<Gig>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsCancelled_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigHasDifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [Test]
        public void GetUpcomingGigsByArtist_GigIsForGivenArtistAndInFuture_ShouldBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);
        }
    }
}
