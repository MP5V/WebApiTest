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
        private readonly ToDoRepository _repository;

        public ToDoController(ToDoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItems>>> GetAll()
        {
            var items = await _repository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItems>> GetById(int id)
        {
            var item = await _repository.GetById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItems>> Create(ToDoItems item)
        {
            await _repository.Create(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToDoItems updatedItem)
        {
            updatedItem.Id = id;
            await _repository.Update(updatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingItem = await _repository.GetById(id);
            if (existingItem == null) return NotFound();

            await _repository.Delete(id);
            return NoContent();
        }
    }
}

