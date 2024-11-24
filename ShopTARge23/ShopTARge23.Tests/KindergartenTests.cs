using Microsoft.EntityFrameworkCore;
using Moq;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using ShopTARge23.Core.Domain;


namespace ShopTARge23.Tests
{
    public class KindergartenTests : TestBase
    {
        [Fact]
        public async Task Should_Create_Kindergarten_With_Valid_Data()
        {
            KindergartenDto dto = new();
            
            dto.GroupName = "Kutsikad";
            dto.ChildrenCount = 22;
            dto.KindergartenName = "Suva";
            dto.Teacher = "Triin";
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;
            
            var result = await SvC<IKindergartenServices>().Create(dto);

            
            Assert.NotNull(result);
            Assert.Equal(dto.GroupName, result.GroupName);
            Assert.Equal(dto.KindergartenName, result.KindergartenName);
        }

        [Fact]
        public async Task ShouldNot_CreateKindergarten_WithMissingData()
        {
            KindergartenDto dto = new();

            dto.GroupName = null;
            dto.ChildrenCount = 15;
            dto.KindergartenName = "Shooters";
            dto.Teacher = "Paula";
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;

            var result = await SvC<IKindergartenServices>().Create(dto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_CreateKindergarten_WithNoChildren()
        {
            KindergartenDto dto = new();

            dto.GroupName = "Sats";
            dto.ChildrenCount = null;
            dto.KindergartenName = "Lepatriinu";
            dto.Teacher = "Mari";
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;

            var result = await SvC<IKindergartenServices>().Create(dto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_CreateKindergarten_InDatabase()
        {
            var options = new DbContextOptionsBuilder<ShopTARge23Context>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using var dbContext = new ShopTARge23Context(options);

            var kindergarten = new Kindergarten
            {
                GroupName = "Kutsikad",
                ChildrenCount = 22,
                KindergartenName = "Suva",
                Teacher = "Triin",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            dbContext.Kindergartens.Add(kindergarten);
            await dbContext.SaveChangesAsync();

            var result = await dbContext.Kindergartens.FirstOrDefaultAsync(k => k.GroupName == "Kutsikad");

            Assert.NotNull(result);
            Assert.Equal(kindergarten.KindergartenName, result.KindergartenName);
        }
    }
}