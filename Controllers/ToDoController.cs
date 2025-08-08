using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using System.IO;
using WebApiTest.Repository;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        /*private static List<ToDoItems> _items = new List<ToDoItems>
        {
            new ToDoItems { Id = 1, Title = "Пример задачи", IsCompleted = false, ImageFileName = "аыаыа.jpg"}
        };*/

        private readonly ToDoRepository _repository;

        public ToDoController()
        {
            _repository = new ToDoRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItems>> GetAll()
        {
            var items = _repository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItems> GetById(int id)
        {
            var item = _repository.GetById(id);
            return item == null ? NotFound() : Ok(item);
        }   

        [HttpPost]
        public ActionResult<ToDoItems> Create(ToDoItems item)
        {
            _repository.Create(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDoItems updatedItem)
        {
            var item = _repository.GetById(id);
            if (item == null) return NotFound();

            updatedItem.Id = item.Id;
            _repository.Update(updatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _repository.GetById(id);
            if (item == null) return NotFound();

            _repository.Delete(id);
            return NoContent();
        }
    }
}

