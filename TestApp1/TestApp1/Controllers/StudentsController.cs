using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

       // private static string connString = "Server=MDL-001;Database=TestApp1DB;User ID=sa;Password=1234; MultipleActiveResultSets=true";       

        public StudentsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students
                .Include(s => s.Department)
                .Include(s => s.School)
                .Include(s => s.District)
                .Include(s => s.Division)
                .OrderBy(s=>s.Division)
                .ThenBy(s=> s.District)
                .ThenBy(s=> s.School)
                .ThenBy(s => s.Department)
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

        public JsonResult GetDepartmentsbySchoolId(int id)
        {
            var departments = _context.Departments.Where(d => d.SchoolId == id).ToList();
            return Json(departments);
        }

        public IActionResult Report()
        {
            var divisions = _context.Divisions.Include(d=>d.Districts).ToList();           

            var schoolList = new List<School>();
            var departmentList = new List<Department>();
            var studentList = new List<Student>();

            foreach(var div in divisions)
            {
                var districts = _context.Districts
                    .Include(d => d.Schools)
                    .Where(d => d.DivisionId == div.Id).ToList();             

                foreach(var dist in districts)
                {
                    var schools = _context.Schools
                        .Include(s => s.Departments)
                        .Where(s => s.DistrictId == dist.Id).ToList();

                    schoolList.AddRange(schools);

                    foreach(var sch in schools)
                    {
                        var departments = _context.Departments
                            .Include(d => d.Students)
                            .Where(d => d.SchoolId == sch.Id)
                            .ToList();

                        departmentList.AddRange(departments);

                        foreach (var dept in departments)
                        {
                            var students = _context.Students
                                .Where(s => s.DepartmentId == dept.Id)
                                .ToList();

                            studentList.AddRange(students);
                        }
                    }
                }
            }

            ViewBag.Divisions = divisions; //district is included in divisions
            ViewBag.Schools = schoolList;
            ViewBag.Departments = departmentList;
            ViewBag.Students = studentList;

            return View();
        }
    }
}

//SqlConnection conn = new SqlConnection(connString);
//var query = @"SELECT Divisions.Name as Division, Districts.Name as District, Schools.Name as School, Departments.Name as Department, COUNT(Students.Id) AS NumberOfStudent
//                          FROM Divisions
//                          LEFT JOIN Districts ON Divisions.Id = Districts.DivisionId
//                          LEFT JOIN Schools ON Districts.Id = Schools.DistrictId
//                          LEFT JOIN Departments ON Schools.Id = Departments.SchoolId
//                          LEFT JOIN Students ON Departments.Id = Students.DepartmentId
//                          GROUP BY Divisions.Name, Districts.Name, Schools.Name, Departments.Name";

//SqlCommand cmd = new SqlCommand(query, conn);
//conn.Open();
//            SqlDataReader sdr = cmd.ExecuteReader();

//var reportList = new List<StudentReport>();

//            while (sdr.Read())
//            {
//                var report = new StudentReport()
//                {
//                    Division = sdr["Division"].ToString(),
//                    District = sdr["District"].ToString(),
//                    School = sdr["School"].ToString(),
//                    Department = sdr["Department"].ToString(),
//                    NoofStudents = Convert.ToInt32(sdr["NumberOfStudent"])
//                };
//reportList.Add(report);
//            }
//            sdr.Close();
//            conn.Close();
            
//            return View(reportList);

//var divisions = _context.Divisions
//                .Include(d => d.Districts)
//                .ToList();

//var result = from div in _context.Divisions
//             join dist in _context.Districts on div.Id equals dist.DivisionId
//             join sch in _context.Schools on dist.Id equals sch.DistrictId
//             join dept in _context.Departments on sch.Id equals dept.SchoolId
//             join s in _context.Students on dept.Id equals s.DepartmentId
//             select new StudentReport { Division = div.Name, District = dist.Name, School = sch.Name, Department = dept.Name, NoofStudents =  };

//            return View(result);


//     var queryGroupMax =
//from student in students
//group student by student.Year into studentGroup
//select new
//{
//    Level = studentGroup.Key,
//    HighestScore =
//    (from student2 in studentGroup
//     select student2.ExamScores.Average()).Max()
//};

//     int count = queryGroupMax.Count();

//var query = from person in people
//            join pet in pets on person equals pet.Owner into gj
//            from subpet in gj.DefaultIfEmpty()
//            select new { person.FirstName, PetName = subpet?.Name ?? String.Empty };

//    var result = languages.GroupJoin(persons, lang => lang.Id, pers => pers.LanguageId,
//(lang, ps) => new { Key = lang.Name, Persons = ps });



//SELECT Divisions.Name as Division, Districts.Name as District, Schools.Name as School, Departments.Name as Department, COUNT(Students.Id) AS NumberOfStudent
//FROM Divisions
//LEFT JOIN Districts ON Divisions.Id = Districts.DivisionId
//LEFT JOIN Schools ON Districts.Id = Schools.DistrictId
//LEFT JOIN Departments ON Schools.Id = Departments.SchoolId
//LEFT JOIN Students ON Departments.Id = Students.DepartmentId
//GROUP BY Divisions.Name, Districts.Name, Schools.Name, Departments.Name;

//SELECT COUNT(Students.Id) AS NumberOfStudent
//FROM Divisions
//LEFT JOIN Districts ON Divisions.Id = Districts.DivisionId
//LEFT JOIN Schools ON Districts.Id = Schools.DistrictId
//LEFT JOIN Departments ON Schools.Id = Departments.SchoolId
//LEFT JOIN Students ON Departments.Id = Students.DepartmentId
//GROUP BY Divisions.Name, Districts.Name, Schools.Name, Departments.Name;