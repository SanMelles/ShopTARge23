using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Data;
using ShopTARge23.Models.Kindergarten;

namespace ShopTARge23.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge23Context _context;
        public KindergartenController
            (
            ShopTARge23Context context
            )
        {
            _context = context;
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
    }
}
