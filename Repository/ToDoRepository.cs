using Dapper;
using System.Collections.Generic;
using System.Linq;
using WebApiTest.Models;
using WebApiTest.Repository;
using WebApiTest.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Repository
{
    public class ToDoRepository
    {
        private readonly DataBaseConnection _db;

        public ToDoRepository()
        {
            _db = new DataBaseConnection();
        }

        public async Task<IEnumerable<ToDoItems>> GetAll()
        {
            using var connection = _db.GetConnection();
            var items = await connection.QueryAsync<ToDoItems>("SELECT * FROM todo_items");
            return items;
        }

        public async Task<ToDoItems> GetById(int id)
        {
            using var connection = _db.GetConnection();
            //connection.Open();
            return await connection.QueryFirstOrDefaultAsync<ToDoItems>("SELECT * FROM todo_items WHERE Id = @Id", new {Id = id});
        }

        public async Task Create(ToDoItems item)
        {
            using var connection = _db.GetConnection();
            //connection.Open();
            var createQiery = "INSERT INTO todo_items (title, iscompleted, imagefilename) VALUES (@Title, @IsCompleted, @ImageFileName)";
            await connection.ExecuteAsync(createQiery, item);
        }

        public async Task Update(ToDoItems item)
        {
            using var connection = _db.GetConnection();
           // connection.Open();
            var updateQuery = "UPDATE todo_items SET title = @Title, iscompleted = @IsCompleted, imagefilename = @ImageFileName WHERE id = @Id";
            await connection.ExecuteAsync(updateQuery, item);
        }

        public async Task Delete(int id)
        {
            using var connection = _db.GetConnection();
            //connection.Open();
            var deleteQuery = "DELETE FROM todo_items where id = @Id";
            await connection.ExecuteAsync(deleteQuery, new { Id = id });
        }
    }
}
