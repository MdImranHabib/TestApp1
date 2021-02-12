using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp1.Models
{
    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DivisionId { get; set; }

        public virtual Division Division { get; set; }
        public virtual List<School> Schools { get; set; }
    }
}
