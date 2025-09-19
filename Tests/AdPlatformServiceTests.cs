using AdPlatform.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Text;

namespace AdPlatformTests
{
    public class AdPlatformServiceTests
    {
        private readonly AdPlatformService _adService;

        public AdPlatformServiceTests()
        {
            _adService = new AdPlatformService();
        }

        [Fact]
        public void LoadFromFile_WithValidData_ShouldReturnCorrectPlatformCount()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var content = "Яндекс.Директ:/ru\nРевдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik\nГазета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            mockFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            mockFile.Setup(f => f.Length).Returns(memoryStream.Length);

            // Act
            var result = _adService.LoadFromFile(mockFile.Object);

            // Assert
            result.Should().Be(3);
        }

        [Fact]
        public void LoadFromFile_WithEmptyFile_ShouldReturnZero()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(""));

            mockFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            mockFile.Setup(f => f.Length).Returns(memoryStream.Length);

            // Act
            var result = _adService.LoadFromFile(mockFile.Object);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void FindAdPlatforms_WithSpecificLocation_ShouldReturnOnlyDirectPlatform()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var content = "Ревдинский рабочий:/ru/svrd/revda";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            mockFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            mockFile.Setup(f => f.Length).Returns(memoryStream.Length);

            _adService.LoadFromFile(mockFile.Object);

            // Act
            var result = _adService.FindAdPlatforms("/ru/svrd/revda");

            // Assert 
            result.Should().ContainSingle().Which.Should().Be("Ревдинский рабочий");
        }

        [Fact]
        public void FindAdPlatforms_WithParentLocation_ShouldReturnPlatformsFromParentAndChild()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var content = "Яндекс.Директ:/ru\nРевдинский рабочий:/ru/svrd/revda\nКрутая реклама:/ru/svrd";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            mockFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            mockFile.Setup(f => f.Length).Returns(memoryStream.Length);

            _adService.LoadFromFile(mockFile.Object);

            // Act
            var result = _adService.FindAdPlatforms("/ru/svrd/revda");

            // Assert
            result.Should().HaveCount(3);
            result.Should().Contain("Яндекс.Директ");
            result.Should().Contain("Ревдинский рабочий");
            result.Should().Contain("Крутая реклама");
        }

        [Fact]
        public void FindAdPlatforms_WithMultiplePlatformsInSameLocation_ShouldReturnAllPlatforms()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var content = "Яндекс.Директ:/ru\nГазета уральских москвичей:/ru/msk,/ru/permobl\nРевдинский рабочий:/ru/svrd/revda";
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            mockFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            mockFile.Setup(f => f.Length).Returns(memoryStream.Length);

            _adService.LoadFromFile(mockFile.Object);

            // Act
            var result = _adService.FindAdPlatforms("/ru/msk");

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain("Яндекс.Директ");
            result.Should().Contain("Газета уральских москвичей");
        }
    }
}