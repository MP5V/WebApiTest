using System.Collections.Generic;
using System.Linq;
using WebApiTest.Models;
using WebApiTest.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace WebApiTest.Repository
{
    public class ToDoRepository
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItems>> GetAll()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItems?> GetById(int id)
        {
            return await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(ToDoItems item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ToDoItems item)
        {
            var existing = await _context.ToDoItems.FindAsync(item.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _context.ToDoItems.FindAsync(id);
            if (entity != null)
            {
                _context.ToDoItems.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
