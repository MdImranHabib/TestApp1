using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp1.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
