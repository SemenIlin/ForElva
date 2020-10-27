using Microsoft.AspNetCore.Mvc;
using ForElva.BLL.Interfaces;
using System.Collections.Generic;

namespace ForElva.Controllers
{
    public class HomeController : Controller
    {
        private readonly static object threadLock = new object();
        private readonly IService _service;

        public HomeController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult ShowMap(string search)
        {
            var path = string.Format("{0}{1}", _service.Url, search);
            return Redirect(path);
        }   
        public IActionResult Save(string search, string fileName, string count)
        {
            try
            {
                var extension = ".txt";
                var resultFileName = fileName + extension;
                lock (threadLock)
                {
                    return File(_service.GetData(search, count), GetMineTypes()[extension], resultFileName);
                }
            }
            catch 
            {
                return View("Error");
            }
        }

        private Dictionary<string, string> GetMineTypes()
        {
            return new Dictionary<string, string>()
            {
                {".txt", "text/plain" },
                {".pdf", "application/pdf" },
                {".doc", "application/vnd.ms-word" },
                {".docx", "application/vnd.ms-word" },
                {".xls", "application/vnd.ms-exel" },
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                {".png", "image/png" },
                {".jpg", "image/jpeg" },
                {".jpeg", "image/jpeg" },
                {".gif", "image/gif" },
                {".csv", "text/csv" }
            };
        }
    }
}
