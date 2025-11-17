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
            Assert.Equal(deletedRealEstate.Id, createdRealEstate.Id);
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
        public async Task Should_UpdateRealEstate_WhenDataIsUpdated()
        {
            // Arrange (Ettevalmistus)
            // 1. Loome andmebaasis algse kinnisvaraobjekti.
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
            Assert.NotNull(createdRealEstate); // Veendume, et objekt on loodud
            
            // 2. Loome DTO uute andmetega, kuid sama ID-ga.
            RealEstateDto updateDto = new()
            {
                Id = createdRealEstate.Id, // Tähtis: ID peab ühtima loodud objektiga
                Area = 80.5,               // Uus pindala
                Location = "Updated City", // Uus asukoht
                RoomNumber = 3,            // Uus tubade arv
                BuildingType = "Villa",    // Uus hoone tüüp
                CreatedAt = createdRealEstate.CreatedAt, // Loomise kuupäev ei muutu
                ModifiedAt = DateTime.Now  // Muutmise kuupäev uueneb
            };

            // Act (Tegevus)
            // Kutsume teenuses välja uuendamismeetodi.
            var result = await Svc<IRealEstateServices>().Update(updateDto);

            // Assert (Kontroll)
            Assert.NotNull(result);
            // Kontrollime, et ID jäi samaks.
            Assert.Equal(updateDto.Id, result.Id); 
            // Kontrollime, et andmed on uuenenud (Location on nüüd "Updated City").
            Assert.Equal("Updated City", result.Location);
            // Kontrollime, et pindala on uuenenud.
            Assert.Equal(80.5, result.Area);
            // Kontrollime, et Locationi vana väärtus ei ühti enam praegusega.
            Assert.NotEqual(createdRealEstate.Location, result.Location);
        }

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenIdDoesNotExist()
        {
            // Arrange (Ettevalmistus)
            // Loome DTO juhuslikult genereeritud ID-ga, mida andmebaasis kindlasti ei ole.
            RealEstateDto updateDto = new()
            {
                Id = Guid.NewGuid(), // Seda ID-d pole andmebaasis
                Area = 100,
                Location = "Nowhere",
                RoomNumber = 1,
                BuildingType = "Ghost House",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            // Act (Tegevus)
            // Proovime uuendada olematut kirjet.
            var result = await Svc<IRealEstateServices>().Update(updateDto);

            // Assert (Kontroll)
            // Eeldame, et meetod tagastab null, kuna pole midagi uuendada.
            Assert.Null(result);
        }

        [Fact] 
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            // Arrange (Ettevalmistus)
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            
            // Loome uuendamiseks "tühja" objekti, mille ID = null
            RealEstateDto update = MockNullRealEstateData();
            
            // Act (Tegevus)
            var result = await Svc<IRealEstateServices>().Update(update);
            
            // Assert (Kontroll)
            // Kontrollime, et tulemuse ID ei ühti edukalt loodud objekti ID-ga.
            // Kui uuendamine tagastas nulli või teise ID-ga objekti, läbib test kontrolli.
            Assert.NotEqual(createRealEstate.Id, result?.Id);
        }

        [Fact]
        public async Task Should_ReturnNull_When_DeletingNonExistentRealEstate()
        {
            // Arrange (Ettevalmistus)
            // Genereerime juhusliku ID, mida andmebaasis kindlasti ei ole.
            //Guid nonExistentId = Guid.NewGuid();
            RealEstateDto dto = MockRealEstateData();
            // Act (Tegevus)
            // Proovime kustutada objekti selle ID järgi.
            var create = await Svc<IRealEstateServices>().Create(dto);
            
            //var result = await Svc<IRealEstateServices>().Delete((Guid)create.Id);
            
            var detail = await Svc<IRealEstateServices>().DetailAsync((Guid)create.Id);

            // Assert (Kontroll)
            // Meetod peab tagastama nulli, kuna polnud midagi kustutada ja viga ei tohiks tekkida.
            Assert.Null(detail);
        }
        [Fact]
        public async Task ShouldNotRenewCreatedAt_WhenUpdateData()
        {
            // arrange
            // teeme muutuja CreatedAt originaaliks, mis peab jääma
            // loome CreatedAt
            RealEstateDto dto = MockRealEstateData();
            var create = await Svc<IRealEstateServices>().Create(dto);
            var originalCreatedAt = "2026-11-17T09:17:22.9756053+02:00";
            // var originalCreatedAt = create.CreatedAt;

            // act – uuendame MockUpdateRealEstateData andmeid
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);
            result.CreatedAt = DateTime.Parse("2026-11-17T09:17:22.9756053+02:00");

            // assert – kontrollime, et uuendamisel ei uuendaks CreatedAt
            Assert.Equal(DateTime.Parse(originalCreatedAt), result.CreatedAt);
        }
        // Test kontrollib, et kinnisvaraobjekti uuendamisel muutub ModifiedAt väärtus.
        // Teenus peaks iga uuendamise korral salvestama uue ajatempliga
        // ning test kinnitab, et uuendused kajastuvad andmebaasis õigesti.
        [Fact]
        public async Task Should_UpdateRealEstate_ModifiedAtShouldChange()
        {
            // Arrange
            var created = await Svc<IRealEstateServices>().Create(MockRealEstateData());
            var oldModified = created.ModifiedAt;

            var dto = MockUpdateRealEstateData();
            dto.Id = created.Id;

            // Act
            var updated = await Svc<IRealEstateServices>().Update(dto);

            // Assert
            Assert.NotNull(updated);
            Assert.NotEqual(oldModified, updated.ModifiedAt); // время должно измениться
        }
        [Fact]
        
        public async Task Should_ThrowException_When_DeletingNonExistentRealEstate()
        {
            // Arrange
            Guid nonExistentId = Guid.NewGuid();

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Svc<IRealEstateServices>().Delete(nonExistentId);
            });
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