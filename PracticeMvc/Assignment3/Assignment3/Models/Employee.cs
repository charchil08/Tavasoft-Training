using System;
using System.Collections.Generic;

namespace Assignment3.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
        }

        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? JobName { get; set; }
        public int? ManagerId { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Commission { get; set; }
        public int? DepId { get; set; }

        public virtual Department? Dep { get; set; }
        public virtual Employee? Manager { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
    }
}
