using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;
using Xunit;

using Assert = Xunit.Assert; 

namespace ShopTARgv24.Spaceships
{
    public class SpaceshipsTest : TestBase
    {
        [Fact]
        public async Task Should_CreateSpaceship_WhenDataIsCorrect()
        {
            // Arrange
            SpaceshipDto dto = MockSpaceshipData();

            // Act
            var result = await Svc<ISpaceshipsServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Id);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Crew, result.Crew);
            Assert.InRange((DateTime)result.CreatedAt, DateTime.Now.AddSeconds(-5), DateTime.Now.AddSeconds(5));
        }

        [Fact]
        public async Task Should_GetByIdSpaceship_WhenReturnsEquals()
        {
            // Arrange
            var dto = MockSpaceshipData();
            var created = await Svc<ISpaceshipsServices>().Create(dto);
            
            // Act
            var received = await Svc<ISpaceshipsServices>().DetailAsync((Guid)created.Id);

            // Assert
            Assert.NotNull(received);
            Assert.Equal(created.Id, received.Id);
            Assert.Equal(created.Name, received.Name);
        }

        [Fact]
        public async Task ShouldNot_GetByIdSpaceship_WhenReturnsNull()
        {
            // Arrange
            var notExistingId = Guid.NewGuid();

            // Act
            var result = await Svc<ISpaceshipsServices>().DetailAsync(notExistingId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateSpaceship_WhenDataIsUpdated()
        {
            // Arrange
            var dto = MockSpaceshipData();
            var created = await Svc<ISpaceshipsServices>().Create(dto);
            
            var context = Svc<ShopTARgv24Context>();
            context.ChangeTracker.Clear();

            var updateDto = MockSpaceshipData();
            updateDto.Id = created.Id;
            updateDto.Name = "Updated Name";
            updateDto.Crew = 123;
            updateDto.CreatedAt = created.CreatedAt;

            // Act
            var result = await Svc<ISpaceshipsServices>().Update(updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal("Updated Name", result.Name);
            Assert.Equal(123, result.Crew);
            Assert.NotEqual(created.ModifiedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task Should_DeleteByIdSpaceship_WhenDeleteSpaceship()
        {
            // Arrange
            var dto = MockSpaceshipData();
            var created = await Svc<ISpaceshipsServices>().Create(dto);

            // Act
            var deleted = await Svc<ISpaceshipsServices>().Delete((Guid)created.Id);
            var check = await Svc<ISpaceshipsServices>().DetailAsync((Guid)created.Id);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(created.Id, deleted.Id);
            Assert.Null(check); 
        }

        private SpaceshipDto MockSpaceshipData()
        {
            return new SpaceshipDto()
            {
                Name = "Millennium Falcon",
                TypeName = "Freighter",
                BuiltDate = DateTime.Now.AddYears(-10),
                Crew = 4,
                EnginePower = 9000,
                Passengers = 2,
                InnerVolume = 500,
                Files = new List<IFormFile>(),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }
    }
}