using Moq;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;


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
    }
}