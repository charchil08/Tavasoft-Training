using System;
using System.Collections.Generic;

namespace Assignment3.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int DepId { get; set; }
        public string? DepName { get; set; }
        public string? DepLocation { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
