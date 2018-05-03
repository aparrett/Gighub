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
    public class GenreRepositoryTests
    {
        private GenreRepository _repository;
        private Mock<DbSet<Genre>> _mockGenres;

        [SetUp]
        public void SetUp()
        {
            _mockGenres = new Mock<DbSet<Genre>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Genres).Returns(_mockGenres.Object);

            _repository = new GenreRepository(mockContext.Object);
        }

        [Test]
        public void GetGenres_WhenCalled_ShouldReturnGenres()
        {
            var genre = new Genre();

            _mockGenres.SetSource(new[] { genre });

            var result = _repository.GetGenres();

            result.Should().Contain(genre);
            result.Should().HaveCount(1);
        }
    }
}
