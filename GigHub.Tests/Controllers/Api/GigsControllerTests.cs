using GigHub.Controllers.api;
using GigHub.Core;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;

        [TestMethod]
        public void TestMethod1()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new GigsController(mockUnitOfWork.Object);
            _controller.MockCurrentUser("1", "user1@domain.com");
        }
    }
}
