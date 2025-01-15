using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SEP.Models;

namespace SEP.Pages.Employee
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            var st = SL2Context.Ins.Employees.Include(x => x.Department).ToList();
            ViewData["std"] = st;
            ViewData["dept"] = SL2Context.Ins.Departments.ToList();
        }
    }
}
