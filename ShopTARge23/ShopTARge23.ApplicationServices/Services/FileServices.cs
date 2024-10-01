using Microsoft.Extensions.Hosting;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;


namespace ShopTARge23.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopTARge23Context _context;

        public FileServices
            (
                IHostEnvironment webHost,
                ShopTARge23Context context
            )
        {
            _webHost = webHost;
            _context = context;
        }


        public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
            {
                Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
            }

            foreach (var file in dto.Files)
            {
                string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);

                    FileToApi path = new FileToApi
                    {
                        Id = Guid.NewGuid(),
                        ExistingFilePath = uniqueFileName,
                        SpaceshipId = spaceship.Id
                    };

                    _context.FileToApi.AddAsync(path);
                }
            }
        }
    }
}