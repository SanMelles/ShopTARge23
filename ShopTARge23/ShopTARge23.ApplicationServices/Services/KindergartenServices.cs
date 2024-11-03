using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;


namespace ShopTARge23.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARge23Context _context;
        private readonly IFileServices _fileServices;

        public KindergartenServices
            (
                ShopTARge23Context context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Kindergarten> DetailsAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new Kindergarten();

            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.Teacher = dto.Teacher;
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;

            await _context.Kindergartens.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(x =>x.Id == id);

            _context.Kindergartens.Remove(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            Kindergarten domain = new();

            domain.Id = dto.Id;
            domain.GroupName = dto.GroupName;
            domain.ChildrenCount = dto.ChildrenCount;
            domain.KindergartenName = dto.KindergartenName;
            domain.Teacher = dto.Teacher;
            domain.CreatedAt = DateTime.Now;
            domain.UpdatedAt = DateTime.Now;

            _context.Kindergartens.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
