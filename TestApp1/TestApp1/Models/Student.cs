using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp1.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsOrgEmailValid", "Students", ErrorMessage = "This email is not valid. Please enter a valid organization email.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string OrganizationEmail { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int SchoolId { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public int DivisionId { get; set; }

        public virtual Department Department { get; set; }
        public virtual School School { get; set; }
        public virtual District District { get; set; }
        public virtual Division Division { get; set; }
    }
}
