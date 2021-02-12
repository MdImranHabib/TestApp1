using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp1.Models
{
    public class Division
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<District> Districts { get; set; }
    }
}
