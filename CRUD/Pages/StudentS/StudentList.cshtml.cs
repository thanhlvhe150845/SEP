using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CRUD.Pages.StudentS
{
    public class StudentList : PageModel
    {
        private readonly HttpClient _httpClient;

        // Tiêm HttpClient vào constructor
        public StudentList(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("APIClient");  // Lấy HttpClient đã cấu hình từ DI container
        }

        public List<Student> Students { get; set; }

        public async Task OnGetAsync()
        {
            // Gọi API để lấy danh sách Student
            var response = await _httpClient.GetStringAsync("api/Student");

            Students = JsonConvert.DeserializeObject<List<Student>>(response);
        }
    }

    // Lớp Student để ánh xạ với dữ liệu API
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string DepartId { get; set; }
        public DateTime? Dob { get; set; }
        public double Gpa { get; set; }
        public Department Depart { get; set; }
    }

    // Lớp Department để ánh xạ với thông tin bộ phận (Department)
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}