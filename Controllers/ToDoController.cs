using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using System.IO;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItems> _items = new List<ToDoItems>
        {
            new ToDoItems { Id = 1, Title = "Пример задачи", IsCompleted = false, ImageFileName = "аыаыа.jpg"}
        };

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItems>> GetAll() => Ok(_items);

        [HttpGet("{id}")]
        public ActionResult<ToDoItems> GetById(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : Ok(item);
        }   

        [HttpPost]
        public ActionResult<ToDoItems> Create(ToDoItems item)
        {
            item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
            _items.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDoItems updatedItem)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            item.Title = updatedItem.Title;
            item.IsCompleted = updatedItem.IsCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            _items.Remove(item);
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var contentType = "image/png"; // или определить по расширению
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, contentType);
        }
    }
}

