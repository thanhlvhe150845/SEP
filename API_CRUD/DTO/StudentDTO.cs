namespace API_CRUD.DTO;

public class StudentDTO
{
    public string Name { get; set; } = null!;
    public bool Gender { get; set; }
    public string DepartId { get; set; } = null!;
    public DateTime? Dob { get; set; }
    public double Gpa { get; set; }
}