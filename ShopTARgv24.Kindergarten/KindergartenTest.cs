using System;
using System.Threading.Tasks;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using Xunit;

namespace ShopTARgv24.Kindergarten
{
    public class KindergartenTest : TestBase
    {
        [Fact]
        public async Task Should_CreateKindergarten_WhenDataIsCorrect()
        {
            // Arrange
            KindergartenDto dto = MockKindergartenData();

            // Act
            var result = await Svc<IKindergartenServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.KindergartenId);
            Assert.Equal(dto.GroupName, result.GroupName);
            Assert.Equal(dto.ChildrenCount, result.ChildrenCount);
            Assert.InRange((DateTime)result.CreatedAt, DateTime.Now.AddSeconds(-5), DateTime.Now.AddSeconds(5));
        }

        [Fact]
        public async Task Should_GetByIdKindergarten_WhenReturnsEquals()
        {
            // Arrange
            var dto = MockKindergartenData();
            var created = await Svc<IKindergartenServices>().Create(dto);

            // Act
            var received = await Svc<IKindergartenServices>().DetailAsync((Guid)created.KindergartenId);

            // Assert
            Assert.NotNull(received);
            Assert.Equal(created.KindergartenId, received.KindergartenId);
            Assert.Equal(created.GroupName, received.GroupName);
        }

        [Fact]
        public async Task ShouldNot_GetByIdKindergarten_WhenReturnsNull()
        {
            // Arrange
            var notExistingId = Guid.NewGuid();

            // Act
            var result = await Svc<IKindergartenServices>().DetailAsync(notExistingId);

            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public async Task Should_DeleteByIdKindergarten_WhenDeleteKindergarten()
        {
            // Arrange
            var dto = MockKindergartenData();
            var created = await Svc<IKindergartenServices>().Create(dto);

            // Act
            var deleted = await Svc<IKindergartenServices>().Delete((Guid)created.KindergartenId);
            var check = await Svc<IKindergartenServices>().DetailAsync((Guid)created.KindergartenId);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(created.KindergartenId, deleted.KindergartenId);
            Assert.Null(check);
        }

        [Fact]
        public async Task ShouldNot_UpdateKindergarten_WhenIdDoesNotExist()
        {
            // Arrange
            KindergartenDto updateDto = MockKindergartenData();
            updateDto.KindergartenId = Guid.NewGuid();

            // Act
            var result = await Svc<IKindergartenServices>().Update(updateDto);

            // Assert
            Assert.Null(result);
        }
        

        private KindergartenDto MockKindergartenData()
        {
            return new KindergartenDto()
            {
                GroupName = "Päikesekiir",
                ChildrenCount = 20,
                KindergartenName = "Tallinna Lasteaed",
                TeacherName = "Mari Tamm",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

        private KindergartenDto MockUpdateKindergartenData()
        {
            return new KindergartenDto()
            {
                GroupName = "Vikerkaar",
                ChildrenCount = 24,
                KindergartenName = "Tartu Lasteaed",
                TeacherName = "Jüri Kask",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }
    }
}