using ShopTARge23.Models.Spaceships;

using Microsoft.AspNetCore.Mvc;

namespace ShopTARge23.Models.Kindergarten
{
    public class ImageViewModel
    {
        public Guid ImageId { get; set; }
        public string FilePath { get; set; }
        public Guid KindergartenId { get; set; }
    }
}
