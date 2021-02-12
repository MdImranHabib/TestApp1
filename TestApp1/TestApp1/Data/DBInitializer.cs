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
        }      

        private static async Task InitializeDivision(ApplicationContext context)
        {
            var division = new Division() {Name = "Dhaka"};        

            context.Add(division);
            await context.SaveChangesAsync();
        }

        private static async Task InitializeDistrict(ApplicationContext context)
        {
            var dist1 = new District() { Name = "Dhaka", DivisionId = 1 };
            context.Add(dist1);

            var dist2 = new District() { Name = "Gazipur", DivisionId = 1 };
            context.Add(dist2);

            await context.SaveChangesAsync();
        }

        private static async Task InitializeSchool(ApplicationContext context)
        {
            var s1 = new School() { Name = "School A", Email = "schoola@gmail.com", DistrictId = 1 };
            var s2 = new School() { Name = "School B", Email = "schoolb@gmail.com", DistrictId = 1 };
            var s3 = new School() { Name = "School C", Email = "schoolc@gmail.com", DistrictId = 2 };
            var s4 = new School() { Name = "School D", Email = "schoold@gmail.com", DistrictId = 2 };

            context.Add(s1);
            context.Add(s2);
            context.Add(s3);
            context.Add(s4);

            await context.SaveChangesAsync();
        }

    }
}
