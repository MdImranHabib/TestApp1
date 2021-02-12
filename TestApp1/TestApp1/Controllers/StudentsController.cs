using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp1.Data;
using TestApp1.Models;

namespace TestApp1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationContext _context;

        public StudentsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students
                .Include(s => s.School)
                .Include(s => s.District)
                .Include(s => s.Division)
                .OrderBy(s=>s.Division)
                .ThenBy(s=> s.District)
                .ThenBy(s => s.School)
                .ToList();
            return View(students);
        }

        public IActionResult Register()
        {
            var divisions = _context.Divisions.ToList();
            ViewBag.Divisions = divisions;

            return View();
        }

        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var divisions = _context.Divisions.ToList();
            ViewBag.Divisions = divisions;

            return View();
        }

        public JsonResult IsOrgEmailValid(string organizationEmail)
        {
            bool result = false;

            if (_context.Schools.Any(s => s.Email.ToLower().Trim() == organizationEmail.ToLower().Trim()))
            {
                result = true;
            }
            return Json(result);
        }
     
        public JsonResult GetDistrictsbyDivisionId(int id)
        {
            var districts = _context.Districts.Where(d => d.DivisionId == id).ToList();
            return Json(districts);
        }

        public JsonResult GetSchoolsbyDistrictId(int id)
        {
            var schools = _context.Schools.Where(s => s.DistrictId == id).ToList();
            return Json(schools);
        }

        public IActionResult Report()
        {
            var divisions = _context.Divisions
                .Include(d => d.Districts)
                .ToList();

            var result = from div in _context.Divisions
                         join dist in _context.Districts on div.Id equals dist.DivisionId
                         join sch in _context.Schools on dist.Id equals sch.DistrictId
                         join s in _context.Students on sch.Id equals s.SchoolId
                         select new StudentReport { Division = div.Name, District = dist.Name, School = sch.Name, Department = s.Department, Student = s.Name };

//     SQL COMMAND   SELECT Divisions.Name as Division, Districts.Name as District, Schools.Name as School, Students.Department as Department, Students.Name Student, COUNT(Students.Id) AS NumberOfStudent
//FROM Divisions
//LEFT JOIN Districts ON Divisions.Id = Districts.DivisionId
//LEFT JOIN Schools ON Districts.Id = Schools.DistrictId
//LEFT JOIN Students ON Schools.Id = Students.SchoolId
//GROUP BY Divisions.Name, Districts.Name, Schools.Name, Students.Department, Students.Name;

//            SELECT COUNT(Students.Id) AS NumberOfStudent
//FROM Divisions
//LEFT JOIN Districts ON Divisions.Id = Districts.DivisionId
//LEFT JOIN Schools ON Districts.Id = Schools.DistrictId
//LEFT JOIN Students ON Schools.Id = Students.SchoolId
//GROUP BY Divisions.Name, Districts.Name, Schools.Name, Students.Department

            return View(result);
        }
    }
}