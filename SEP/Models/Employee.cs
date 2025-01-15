using System;
using System.Collections.Generic;

namespace SEP.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public int? DepartmentId { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public virtual Department? Department { get; set; }
    }
}
