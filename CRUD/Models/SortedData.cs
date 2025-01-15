using Microsoft.AspNetCore.Mvc;

namespace CRUD.Models
{
    public class SortedData : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
