using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CRUD.Pages.Stds
{
    public class ReportModel : PageModel
    {
        private readonly PRN211_1Context _context;

        // Property to hold the field selected by the user
        [BindProperty]
        public string Field { get; set; }

        // Property to hold the sorted data for the report
        public SortedDataModel SortedData { get; set; }

        public ReportModel(PRN211_1Context context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Initialize the SortedData on initial page load
            SortedData = new SortedDataModel();
        }

        public void OnPost()
        {
            // Initialize or clear the SortedData
            SortedData = new SortedDataModel();

            // Query data from the database
            var studentData = _context.Students.Include(s => s.Depart).ToList();

            // Process the data based on the selected field
            if (!string.IsNullOrEmpty(Field))
            {
                if (Field == "Gender")
                {
                    // Group by gender and count
                    SortedData.Data = studentData.GroupBy(s => s.Gender ? "Male" : "Female")
                        .ToDictionary(g => g.Key, g => g.Count());
                }
                else if (Field == "Department")
               {
            // Group by department and count, then order by count descending
            SortedData.Data = studentData
                .GroupBy(s => s.Depart.Name) // Group by department name
                .Select(g => new { Department = g.Key, Count = g.Count() , CountMale = g.Where(c => c.Gender).Count()}) // Get department name and count of students
                .OrderByDescending(g => g.Count) // Order by number of students in each department, descending
                .ToDictionary(g => g.Department, g => g.Count);

            SortedData.DataList = studentData
                .GroupBy(s => s.Depart.Name) // Group by department name
                .Select(g => new { Department = g.Key, Count = g.Count(), CountMale = g.Where(c => c.Gender).Count(), CountFeMale = g.Where(c => !c.Gender).Count() }) // Get department name and count of students
                .OrderByDescending(g => g.Count) // Order by number of students in each department, descending
                .Select(c => new CountDataGender
                {
                    Department = c.Department,
                    Count = c.Count,
                    CountFeMale = c.CountFeMale,
                    CountMale = c.CountMale,
                }).ToList();
                }
                else if (Field == "Gpa")
                {
                    // Get all GPA values and count occurrences
                    var gpaData = studentData
                        .GroupBy(s => s.Gpa) // Group by each GPA value
                        .Select(g => new { Gpa = g.Key, Count = g.Count() }) // Get GPA and count occurrences
                        .OrderByDescending(g => g.Count) // Order by the count of students (number of students with that GPA) in descending order
                        .ToList();

                    // Store in the SortedData dictionary with formatted GPA
                    SortedData.Data = gpaData.ToDictionary(g => g.Gpa.ToString("0.0"), g => g.Count);
                }
            }
        }

        // Helper method to categorize GPA into ranges
        //private string GetGpaRange(double gpa)
        //{
        //    if (gpa >= 3.5)
        //        return "3.5 and above";
        //    if (gpa >= 3.0)
        //        return "3.0 - 3.4";
        //    return "Below 3.0";
        //}
    }

    // Define the model for SortedData
    public class SortedDataModel
    {
        public Dictionary<string, int> Data { get; set; } = new Dictionary<string, int>();
        public List<CountDataGender> DataList { get; set; } = new List<CountDataGender>();
    }

    public class CountDataGender
    {
        public string Department { get; set; }
        public int Count { get; set; }
        public int CountMale { get; set; }
        public int CountFeMale { get; set; }
    }
}
