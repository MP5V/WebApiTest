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

        public IEnumerable<ToDoItems> GetAll()
        {
            using var connection = _db.GetConnection();
            connection.Open();
            return connection.Query<ToDoItems>("SELECT * FROM todo_items");
        }

        public ToDoItems GetById(int id)
        {
            using var connection = _db.GetConnection();
            connection.Open();
            return connection.QueryFirstOrDefault<ToDoItems>("SELECT * FROM todo_items WHERE Id = @Id", new {Id = id});
        }

        public void Create(ToDoItems item)
        {
            using var connection = _db.GetConnection();
            connection.Open();
            var createQiery = "INSERT INTO todo_items (title, iscompleted, imagefilename) VALUES (@Title, @IsCompleted, @ImageFileName)";
            connection.Execute(createQiery, item);
        }

        public void Update(ToDoItems item)
        {
            using var connection = _db.GetConnection();
            connection.Open();
            var updateQuery = "UPDATE todo_items SET title = @Title, iscompleted = @IsCompleted, imagefilename = @ImageFileName WHERE id = @Id";
            connection.Execute(updateQuery, item);
        }

        public void Delete(int id)
        {
            using var connection = _db.GetConnection();
            connection.Open();
            var deleteQuery = "DELETE FROM todo_items where id = @Id";
            connection.Execute(deleteQuery, new { Id = id });
        }
    }
}
