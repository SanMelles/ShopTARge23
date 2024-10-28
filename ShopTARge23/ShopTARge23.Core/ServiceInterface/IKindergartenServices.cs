using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface IKindergartenServices
    {
        Task<Kindergarten> DetailsAsync(Guid id);
        Task<Kindergarten> Create(KindergartenDto dto);
        Task<Kindergarten> Update(KindergartenDto dto);
        Task<Kindergarten> Delete(Guid id);
    }
}
