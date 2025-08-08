using Dapper;
using System.Collections.Generic;
using System.Linq;
using WebApiTest.Models;
using WebApiTest.Repository;
using WebApiTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebApiTest.Repository
{
    public class ImageRepository
    {
        private readonly string _imageFolderPath;
        private readonly DataBaseConnection _db;

        public ImageRepository()
        {
            _db = new DataBaseConnection();
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");
            if (!Directory.Exists(_imageFolderPath))
                Directory.CreateDirectory(_imageFolderPath);
        }

        public string GetImagePath(string fileName)
        {
            return Path.Combine(_imageFolderPath, fileName);
        }

        public bool ImageExists(string fileName)
        {
            return File.Exists(GetImagePath(fileName));
        }

        public void SaveImage(byte[] imageBytes, string fileName)
        {
            File.WriteAllBytes(GetImagePath(fileName), imageBytes);
        }

        public void DeleteImage(string fileName)
        {
            var path = GetImagePath(fileName);
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}

