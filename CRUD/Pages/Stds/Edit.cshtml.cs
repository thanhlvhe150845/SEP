using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;
using Microsoft.IdentityModel.Tokens;

namespace CRUD.Pages.Stds
{
    public class EditModel : PageModel
    {


        [BindProperty]
        public Student Student { get; set; } = default!;

        public void OnGet(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewData["id"] = "Goi id = null";
            }
            else
            {
                try
                {
                    int Id = int.Parse(id);
                    ViewData["id"] = id;
                    ViewData["Depart"] = PRN211_1Context.Ins.Departments.ToList();
                    Student = PRN211_1Context.Ins.Students.Find(Id);
                }
                catch (Exception e)
                {
                    ViewData["Depart"] = PRN211_1Context.Ins.Departments.ToList();
                    ViewData["id"] = "Id is not a number";
                }
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            int Id;
            if (!int.TryParse(id, out Id))
            {
                return BadRequest();
            }

            var studentToUpdate = PRN211_1Context.Ins.Students.Find(Id);
            if (studentToUpdate == null)
            {
                return NotFound();
            }

            studentToUpdate.Name = Student.Name;
            studentToUpdate.Gender = Student.Gender;
            studentToUpdate.DepartId = Student.DepartId;
            studentToUpdate.Dob = Student.Dob;
            studentToUpdate.Gpa = Student.Gpa;

            PRN211_1Context.Ins.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}