using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEP.Models;

namespace SEP.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        public IActionResult OnGet(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewData["id"] = "ban da goi id=null";
            }
            else
            {
                try
                {
                    int Id = int.Parse(id);
                    var x = SL2Context.Ins.Employees.Find(Id);
                    if (x != null)
                    {
                        SL2Context.Ins.Employees.Remove(x);
                        SL2Context.Ins.SaveChanges();

                    }
                }
                catch (Exception)
                {
                    ViewData["id"] = "ban da truyn ko hop le";
                }
            }
            return Redirect("~/Employee/Index");
        }
    }
    }
}
