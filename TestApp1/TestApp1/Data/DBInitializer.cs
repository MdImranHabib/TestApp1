using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp1.Models;

namespace TestApp1.Data
{
    public class DBInitializer
    {
        public static async Task Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            await InitializeDivision(context);
            await InitializeDistrict(context);
            await InitializeSchool(context);
            await InitializeDepartment(context);
            await StudentMaster(context);
        }       

        private static async Task InitializeDivision(ApplicationContext context)
        {
            var division = new Division() {Name = "DHAKA"};        

            context.Add(division);
            await context.SaveChangesAsync();
        }

        private static async Task InitializeDistrict(ApplicationContext context)
        {
            var dist1 = new District() { Name = "Dhaka", DivisionId = 1 };         
            var dist2 = new District() { Name = "Gazipur", DivisionId = 1 };

            context.AddRange(dist1, dist2);
            await context.SaveChangesAsync();
        }

        private static async Task InitializeSchool(ApplicationContext context)
        {
            var s1 = new School() { Name = "School A", Email = "schoola@gmail.com", DistrictId = 1 };
            var s2 = new School() { Name = "School B", Email = "schoolb@gmail.com", DistrictId = 1 };
            var s3 = new School() { Name = "School C", Email = "schoolc@gmail.com", DistrictId = 2 };
            var s4 = new School() { Name = "School D", Email = "schoold@gmail.com", DistrictId = 2 };

            context.AddRange(s1, s2, s3, s4);         
            await context.SaveChangesAsync();
        }

        private static async Task InitializeDepartment(ApplicationContext context)
        {
            var d1 = new Department() { Name = "Science", SchoolId = 1 };
            var d2 = new Department() { Name = "Business", SchoolId = 1 };
            var d3 = new Department() { Name = "Arts", SchoolId = 1 };
            
            var d4 = new Department() { Name = "Science", SchoolId = 2 };
            var d5 = new Department() { Name = "Business", SchoolId = 2 };
            var d6 = new Department() { Name = "Arts", SchoolId = 2 };

            var d7 = new Department() { Name = "Science", SchoolId = 3 };
            var d8 = new Department() { Name = "Business", SchoolId = 3 };
            var d9 = new Department() { Name = "Arts", SchoolId = 3 };

            var d10 = new Department() { Name = "Science", SchoolId = 4 };
            var d11 = new Department() { Name = "Business", SchoolId = 4 };
            var d12 = new Department() { Name = "Arts", SchoolId = 4 };

            context.AddRange(d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12);
            await context.SaveChangesAsync();
        }

        private static async Task StudentMaster(ApplicationContext context)
        {
            var s1 = new StudentMaster()
            {
                Division = "DHAKA",
                District = "Dhaka",
                School = "School A",
                Department = "Arts",
                Name = "Shohag"
            };

            var s2 = new StudentMaster()
            {
                Division = "DHAKA",
                District = "Dhaka",
                School = "School A",
                Department = "Business",
                Name = "Shahin"
            };

            var s3 = new StudentMaster()
            {
                Division = "DHAKA",
                District = "Dhaka",
                School = "School A",
                Department = "Arts",
                Name = "Shamim"
            };

            var s4 = new StudentMaster()
            {
                Division = "DHAKA",
                District = "Gazipur",
                School = "School B",
                Department = "Arts",
                Name = "Niloy"
            };

            var s5 = new StudentMaster()
            {
                Division = "DHAKA",
                District = "Gazipur",
                School = "School B",
                Department = "Science",
                Name = "Nayan"
            };

            context.AddRange(s1, s2, s3, s4, s5);
            await context.SaveChangesAsync();
        }
    }
}
