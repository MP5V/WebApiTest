using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Repository;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ImageRepository _imageRepo;

        private readonly string _uploadsFolder;

        public ImageController(ImageRepository imageRepo)
        {
            _imageRepo = imageRepo;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");
            if (!Directory.Exists(_uploadsFolder))
                Directory.CreateDirectory(_uploadsFolder);
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            if (!_imageRepo.ImageExists(fileName))
                return NotFound();

            var filePath = _imageRepo.GetImagePath(fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, contentType);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");

            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{System.Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            try
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var bytes = ms.ToArray();
                    await _imageRepo.SaveImage(bytes, uniqueFileName);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка при сохранении файла");
            }

            return Ok(new { fileName = uniqueFileName });
        }
    }
}
