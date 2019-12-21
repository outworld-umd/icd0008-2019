using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExamApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // FROM EXAM:
            // dotnet ef migrations add InitialDbCreation --project DAL --startup-project ExamApp
            // dotnet ef database update --project DAL --startup-project ExamApp
            // dotnet ef migrations remove
            // FROM EXAM APP:
            // dotnet aspnet-codegenerator razorpage -m Person -dc AppDbContext -udl -outDir Pages/Persons --referenceScriptLibraries
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}