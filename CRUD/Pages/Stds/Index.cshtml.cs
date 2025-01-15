using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;
using System.Xml.Linq;
using System.Globalization;
using System.Text;

namespace CRUD.Pages.Stds
{
    public class IndexModel : PageModel
    {
        private readonly CRUD.Models.PRN211_1Context _context;

        public IndexModel(CRUD.Models.PRN211_1Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get; set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            //if (_context.Students != null)
            //{
            //    Student = await _context.Students
            //    .Include(s => s.Depart).ToListAsync();
            //}
            


            if (!String.IsNullOrEmpty(SearchString))
            {
                string normalizedText = RemoveDiacritics(SearchString);

                var searchTerm = normalizedText.ToLower();

                Student = _context.Students.AsEnumerable()
              .Where(e => RemoveDiacritics(e.Name).ToLower().Contains(normalizedText)
                        ).ToList()
              ;

            }
            else
            {
                Student = await _context.Students.Include(s => s.Depart).ToListAsync();
            }

        }
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            string a = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            return a;
        }
    }
}