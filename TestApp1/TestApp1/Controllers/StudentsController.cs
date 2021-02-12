using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp1.Data;

namespace TestApp1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationContext _context;

        public StudentsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            var divisions = _context.Divisions.ToList();
            ViewBag.Divisions = divisions;

            return View();
        }
    }
}