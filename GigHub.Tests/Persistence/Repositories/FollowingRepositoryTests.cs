using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestFixture]
    public class FollowingRepositoryTests
    {
        private FollowingRepository _repository;
        private Mock<DbSet<Following>> _mockFollowings;

        [SetUp]
        public void SetUp()
        {
            _mockFollowings = new Mock<DbSet<Following>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Followings).Returns(_mockFollowings.Object);

            _repository = new FollowingRepository(mockContext.Object);
        }

        [Test]
        public void GetFollowing_FollowingForDifferentFollower_ShouldNotBeReturned()
        {
            var following = new Following { FollowerId = "1", FolloweeId = "2" };

            _mockFollowings.SetSource(new[] { following });

            var followingFromRepository = _repository.GetFollowing("3", "2");

            followingFromRepository.Should().BeNull();
        }

        [Test]
        public void GetFollowing_FollowingForDifferentFollowee_ShouldNotBeReturned()
        {
            var following = new Following { FollowerId = "1", FolloweeId = "2" };

            _mockFollowings.SetSource(new[] { following });

            var followingFromRepository = _repository.GetFollowing("1", "3");

            followingFromRepository.Should().BeNull();
        }

        [Test]
        public void GetFollowing_FollowingForFolloweeAndFollower_ShouldBeReturned()
        {
            var following = new Following { FollowerId = "1", FolloweeId = "2" };

            _mockFollowings.SetSource(new[] { following });

            var followingFromRepository = _repository.GetFollowing("1", "2");

            followingFromRepository.Should().BeOfType<Following>();
        }
    }
}
