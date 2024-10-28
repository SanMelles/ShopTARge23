using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Data;
using ShopTARge23.Models.Kindergarten;
using ShopTARge23.Core.ServiceInterface;

namespace ShopTARge23.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge23Context _context;
        private readonly IKindergartenServices _services;
        public KindergartenController
            (
            ShopTARge23Context context,
            IKindergartenServices kindergartenServices
            )
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
        }
        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    KindergartenName = x.KindergartenName,
                    Teacher = x.Teacher,
                    CreatedAt = x.CreatedAt,
                });
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailsAsync(id);

            if (kindergarten == null)
            {
                return View("Error");
            }

            var vm = new KindergartenDetailsViewModel();

            vm.Id = kindergarten.id;
            vm.GroupName = kindergarten.groupName;
            vm.ChildrenCount = kindergarten.childrenCount;
            vm.KindergartenName = kindergarten.kindergartenName;
            vm.Teacher = kindergarten.Teacher;
            vm.CreatedAt = kindergarten.createdAt;
            vm.UpdatedAt = kindergarten.updatedAt;

            return View(vm);
        }
    }
}
