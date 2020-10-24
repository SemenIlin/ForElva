using Microsoft.AspNetCore.Mvc;
using ForElva.BLL.Interfaces;

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
                lock (threadLock)
                {
                    _service.Save(search, fileName, count);
                }

                return RedirectToAction("Index");
            }
            catch 
            {
                return View("Error");
            }
        }
    }
}
