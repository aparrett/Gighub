

using FluentAssertions;
using GigHub.Controllers.api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestFixture]
    public class FollowingsControllerTests
    {
        private Mock<IFollowingRepository> _mockRepository;
        private FollowingsController _controller;
        private string _userId;


        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IFollowingRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Followings).Returns(_mockRepository.Object);

            _controller = new FollowingsController(mockUnitOfWork.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [Test]
        public void Follow_AlreadyFollowingUser_ShouldReturnBadRequest()
        {
            var following = new Following();
            _mockRepository.Setup(r => r.GetFollowing(_userId, "2")).Returns(following);

            var result = _controller.Follow(new FollowingDto { FolloweeId = "2" });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [Test]
        public void Follow_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Follow(new FollowingDto { FolloweeId = "2" });

            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void Unfollow_UserNotAlreadyFollowing_ShouldReturnNotFound()
        {
            var result = _controller.Unfollow("1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void Unfollow_ValidRequest_ShouldReturnOk()
        {
            var following = new Following();
            _mockRepository.Setup(r => r.GetFollowing(_userId, "2")).Returns(following);

            var result = _controller.Unfollow("2");

            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [Test]
        public void Unfollow_ValidRequest_ShouldReturnIdOfUnfollowedUser()
        {
            var following = new Following();
            _mockRepository.Setup(r => r.GetFollowing(_userId, "2")).Returns(following);

            var result = (OkNegotiatedContentResult<string>) _controller.Unfollow("2");

            result.Content.Should().Be("2");
        }
    }
}
