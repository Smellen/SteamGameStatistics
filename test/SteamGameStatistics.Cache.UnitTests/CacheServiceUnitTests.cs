using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SteamGameStatistics.Cache.UnitTests
{
    public class CacheServiceUnitTests
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheService _service;

        public CacheServiceUnitTests()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            _memoryCache = serviceProvider.GetService<IMemoryCache>();

            _service = new CacheService(_memoryCache);
        }

        [Fact]
        public void Create_AddValidKey_CacheCreatedSuccessfully()
        {
            // Arrange

            // Act
            _service.Create("createTest", "testobj");

            // Assert
            var hit = _service.TryGet("createTest");
            hit.Should().NotBeNull();
            (hit as string).Should().Be("testobj");
        }

        [Fact]
        public void Delete_DeleteACacheHit_ObjectRemovedSuccessfully()
        {
            // Arrange
            _memoryCache.Set("deleteTest", "deleteTestObject");

            // Act
            _service.Delete("deleteTest");

            // Assert
            var hit = _service.TryGet("deleteTest");
            hit.Should().BeNull();
        }

        [Fact]
        public void TryGet_TryGetAValidHit_ObjectReturnedSuccesfully()
        {
            // Arrange
            _memoryCache.Set("getTest", "getTestObject");

            // Act
            var hit = _service.TryGet("getTest");

            // Assert
            hit.Should().NotBeNull();
            (hit as string).Should().Be("getTestObject");
        }
    }
}
