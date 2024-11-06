using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Data;
using ShopTARge23.Models.Kindergarten;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Core.Dto;

namespace ShopTARge23.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge23Context _context;
        private readonly IKindergartenServices _kindergartenServices;
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
                    Teacher = x.Teacher
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                Teacher = vm.Teacher,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                FileToApiDtos = vm.Image
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.FilePath,
                        KindergartenId = x.KindergartenId
                    }).ToArray()
            };

            var result = await _kindergartenServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailsAsync(id);

            if (kindergarten == null)
            {
                return View("Error");
            }

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == id)
                .Select(y => new ShopTARge23.Models.Kindergarten.KindergartenImageViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new KindergartenDetailsViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                Teacher = kindergarten.Teacher,
                CreatedAt = kindergarten.CreatedAt,
                UpdatedAt = kindergarten.UpdatedAt,
            };
            vm.Images.AddRange(images);


            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailsAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == id)
                .Select(y => new ShopTARge23.Models.Kindergarten.KindergartenImageViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new KindergartenCreateUpdateViewModel();

            vm.Id = kindergarten.Id;
            vm.GroupName = kindergarten.GroupName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
            vm.Teacher = kindergarten.Teacher;
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto()
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                Teacher = vm.Teacher,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                FileToApiDtos = vm.Image
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.FilePath,
                        KindergartenId = x.KindergartenId
                    }).ToArray()
            };

            var result = await _kindergartenServices.Update(dto);

            if(result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailsAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.KindergartenId == id)
                .Select(y => new ShopTARge23.Models.Kindergarten.KindergartenImageViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new KindergartenDeleteViewModel
            {
                Id = kindergarten.Id,
                GroupName = kindergarten.GroupName,
                ChildrenCount = kindergarten.ChildrenCount,
                KindergartenName = kindergarten.KindergartenName,
                Teacher = kindergarten.Teacher,
                CreatedAt = kindergarten.CreatedAt,
                UpdatedAt = kindergarten.UpdatedAt,
            };
            vm.ImageViewModels.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var kindergarten = await _kindergartenServices.Delete(id);

            if(kindergarten == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
