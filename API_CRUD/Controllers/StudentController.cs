using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_CRUD.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_CRUD.Models;

namespace API_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly PRN211Context _context;

        public StudentController(PRN211Context context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            // Lấy danh sách sinh viên và kèm theo thông tin Department
            var students = await _context
                .Students
                .Include(s => s.Depart)
                .Select(x => new Student() {
                    Id = x.Id,
                    Name = x.Name,
                    Gender = x.Gender,
                    Dob = x.Dob,
                    Gpa = x.Gpa,
                    DepartId = x.DepartId,
                    Depart = x.Depart
                })
                .ToListAsync();
                
              
            return students;
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }

            // Lấy sinh viên kèm theo thông tin Department
            var student = await _context.Students.Include(s => s.Depart)
                                                  .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDTO student)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'PRN211Context.Students' is null.");
            }

            var department = await _context.Departments.FindAsync(student.DepartId);
            if (department == null)
            {
                return BadRequest("Invalid Department Id");
            }

            int lastId = _context.Students
                .OrderByDescending(s => s.Id) 
                .FirstOrDefault()?.Id ?? 0;    

            lastId += 1;  

            Student s = new Student()
            {
                Id = lastId,
                DepartId = student.DepartId,
                Gender = student.Gender,
                Name = student.Name,
                Dob = student.Dob,
                Gpa = student.Gpa
            };

            _context.Students.Add(s);
            await _context.SaveChangesAsync();

            return StatusCode(200, new { id = s.Id });
        }


        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
