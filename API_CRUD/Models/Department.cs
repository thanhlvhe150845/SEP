using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace API_CRUD.Models
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
    }
}
