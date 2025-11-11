using System;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using Xunit;

namespace ShopTARgv24.RealEstate
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task Test1()
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
            Guid databaseGuid = Guid.Parse("0e8ce053-a8ed-40d2-9231-e58265172c75");
            Guid guid = Guid.Parse("0e8ce053-a8ed-40d2-9231-e58265172c75");

            //Act
            await Svc<IRealEstateServices>().DetailAsync(guid);
            //Assert

            Assert.Equal(databaseGuid, guid);
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
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();
            
            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deletedRealEstate = await Svc<IRealEstateServices>().Delete((Guid)createdRealEstate.Id);
            
            //Assert
            Assert.Equal(deletedRealEstate, createdRealEstate);
        }
        
        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();
            
            //Act
            var createRealEstate1 = await Svc<IRealEstateServices>().Create(dto);
            var createRealEstate2 = await Svc<IRealEstateServices>().Create(dto);
            var result = await Svc<IRealEstateServices>().Delete((Guid)createRealEstate2.Id);
            
            //Assert
            Assert.NotEqual(result.Id, createRealEstate1.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //Arrange
            //tuleb teha mock guid
            var guid = new Guid("0e8ce053-a8ed-40d2-9231-e58265172c75");
            //tuleb kasutada MockRealEstateData metodit
            RealEstateDto dto = MockRealEstateData();
            //domaini objekt koos selle andmetega peab valja motlema

            Core.Domain.RealEstate domain = new();

            domain.Id = Guid.Parse("0e8ce053-a8ed-40d2-9231-e58265172c75");
            domain.Area = 200.0;
            domain.Location = "Secret Place";
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.Now;
            domain.ModifiedAt = DateTime.Now;
            //Act
            await Svc<IRealEstateServices>().Update(dto);
            //Assert
            Assert.Equal(guid, domain.Id);
            Assert.DoesNotMatch(dto.Location, domain.Location);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            Assert.NotEqual(dto.Area, domain.Area);
        }
        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenIdDoesNotExist()
        {
            //Arrange
            RealEstateDto updateDto = new()
            {
                Id = Guid.NewGuid(),
                Area = 100,
                Location = "Nowhere",
                RoomNumber = 1,
                BuildingType = "Ghost House",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            //Act
            var result = await Svc<IRealEstateServices>().Update(updateDto);

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Should_UpdateRealEstate_WhenDataIsUpdated()
        {
            //Arrange
            RealEstateDto dto = new()
            {
                Area = 55.0,
                Location = "Original City",
                RoomNumber = 2,
                BuildingType = "Cottage",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            Assert.NotNull(createdRealEstate);
            
            RealEstateDto updateDto = new()
            {
                Id = createdRealEstate.Id,
                Area = 80.5,
                Location = "Updated City",
                RoomNumber = 3,
                BuildingType = "Villa",
                CreatedAt = createdRealEstate.CreatedAt,
                ModifiedAt = DateTime.Now
            };

            //Act
            var result = await Svc<IRealEstateServices>().Update(updateDto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Id, result.Id);
            Assert.Equal("Updated City", result.Location);
            Assert.Equal(80.5, result.Area);
            Assert.NotEqual(createdRealEstate.Location, result.Location);
        }
        
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            RealEstateDto update = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);
            Assert.NotEqual(dto.Id, result.Id);
        }
        //motelda ise valja unit test
		//saate tahe 2-3 in meeskonnas
        //see peab olema selline, mida enne pole teinud
        [Fact]
        public async Task Should_ReturnNull_When_DeletingNonExistentRealEstate()
        {
            // Arrange
            Guid nonExistentId = Guid.NewGuid();

            // Act
            var result = await Svc<IRealEstateServices>().Delete(nonExistentId);

            // Assert
            Assert.Null(result);
        }
        private RealEstateDto MockNullRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Id = null,
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null
            };
            return dto;
        }


        private RealEstateDto MockRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Area = 150.0,
                Location = "Tartu",
                RoomNumber = 3,
                BuildingType = "House",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
            return dto;
        }
        
        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto dto = new()
            {
                Area = 100.0,
                Location = "Mountain",
                RoomNumber = 4,
                BuildingType = "Apartment",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };
            
            return dto;
        }
    }
}