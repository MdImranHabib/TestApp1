using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp1.Models
{
    public class School
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
