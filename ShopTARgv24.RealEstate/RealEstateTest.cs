using System;
using System.Threading.Tasks;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using Xunit;

namespace ShopTARgv24.RealEstate
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task Test1() // <--- ИСПРАВЛЕНО (был async void)
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Area = 120.5;
            dto.Location = "Downtown";
            dto.RoomNumber = 3;
            dto.BuildingType = "Apartment";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            //Act

            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result); // <--- ИСПРАВЛЕНО (был Assert.Null)
        }
        
        [Fact]
        public async Task Should_CreateRealEstate_WhenDataIsCorrect()
        {
            //Arrange
            RealEstateDto dto = new();

            dto.Area = 120.5;
            dto.Location = "Downtown";
            dto.RoomNumber = 3;
            dto.BuildingType = "Apartment";
            dto.CreatedAt = DateTime.Now;
            dto.ModifiedAt = DateTime.Now;

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result); 
            Assert.Equal("Downtown", result.Location);
            Assert.Equal(3, result.RoomNumber);
        }
        
        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEquals()
        {
            //Arrange
            var dto = new RealEstateDto
            {
                Location = "TestLocation_GetById",
                Area = 100,
                RoomNumber = 1,
                BuildingType = "Test",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            Assert.NotNull(createdRealEstate); 

            var realEstateId = createdRealEstate.Id;

            //Act
            var result = await Svc<IRealEstateServices>().DetailAsync(realEstateId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(realEstateId, result.Id);
            Assert.Equal("TestLocation_GetById", result.Location);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEquals()
        {
            //Arrange
            var nonExistentId = Guid.NewGuid(); 

            //Act
            var result = await Svc<IRealEstateServices>().DetailAsync(nonExistentId);

            //Assert
            Assert.Null(result); 
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenRealEstateExists()
        {
            //Arrange
            var dto = new RealEstateDto { Location = "ToBeDeleted", Area = 10, RoomNumber = 1, BuildingType = "DeleteTest" };
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            Assert.NotNull(createdRealEstate);
            
            var realEstateId = createdRealEstate.Id;

            //Act
            var deletedResult = await Svc<IRealEstateServices>().Delete(realEstateId);
            
            //Assert
            Assert.NotNull(deletedResult); 
            Assert.Equal(realEstateId, deletedResult.Id);

            var getResultAfterDelete = await Svc<IRealEstateServices>().DetailAsync(realEstateId);
            Assert.Null(getResultAfterDelete); 
        }
        
        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenIdIsNotFound()
        {
            //Arrange
            var nonExistentId = Guid.NewGuid();

            //Act
            var result = await Svc<IRealEstateServices>().Delete(nonExistentId);

            //Assert
            Assert.Null(result); 
        }
    }
}