using System;
using System.Collections.Generic;
using

System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.Controllers
{
    public class DatabaseExampleController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseExampleController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            //DatabaseOperations();
           // DeleteAllData();
        }

        public IActionResult Index()
        {
            return View();
        }

        
     public void DatabaseOperations()
        {

            try
            {
               // DeleteAllData();

            
                // CREATE operation
               //  Insert dummy data for graduate
                var graduate1 = new Graduate { FirstName = "John", LastName = "Doe", ResearchTopic = "Data Science" };
                var graduate2 = new Graduate { FirstName = "Jane", LastName = "Smith", ResearchTopic = "Artificial Intelligence" };
                var graduate3 = new Graduate { FirstName = "Bob", LastName = "Johnson", ResearchTopic = "Data Science" };
                var graduate4 = new Graduate { FirstName = "Alice", LastName = "Williams", ResearchTopic = "Artificial Intelligence" };
                var graduate5 = new Graduate { FirstName = "Charlie", LastName = "Brown", ResearchTopic = "Data Science" };

                _dbContext.Graduates.AddRange(graduate1, graduate2, graduate3, graduate4, graduate5);
                
                
                //* Insert dummy data for project document
                var projectDocument1 = new ProjectDocument { Title = "Research Paper 1", ResearchTopic = "Data Science", PublishedDate = new DateTime(2022, 01, 01), GraduateId = graduate1.GraduateId };
                var projectDocument2 = new ProjectDocument { Title = "Thesis 1", ResearchTopic = "Artificial Intelligence", PublishedDate = new DateTime(2022, 01, 01), GraduateId = graduate1.GraduateId };
                var projectDocument3 = new ProjectDocument { Title = "Case Study 1", ResearchTopic = "Data Science", PublishedDate = new DateTime(2022, 01, 01), GraduateId = graduate2.GraduateId };
                var projectDocument4 = new ProjectDocument { Title = "Project Report 1", ResearchTopic = "Artificial Intelligence", PublishedDate = new DateTime(2022, 01, 01), GraduateId = graduate4.GraduateId };
                var projectDocument5 = new ProjectDocument { Title = "Experiment Results", ResearchTopic = "Data Science", PublishedDate = new DateTime(2022, 01, 01), GraduateId = graduate3.GraduateId };

                _dbContext.ProjectDocuments.AddRange(projectDocument1, projectDocument2, projectDocument3, projectDocument4, projectDocument5);
              
               
                // Insert dummy data for user
                var user1 = new User { FirstName = "David", LastName = "Lee" };
                var user2 = new User { FirstName = "Emily", LastName = "Clark" };
                var user3 = new User { FirstName = "Michael", LastName = "Wang" };
                var user4 = new User { FirstName = "Sophia", LastName = "Garcia" };
                var user5 = new User { FirstName = "Ethan", LastName = "Taylor" };

                _dbContext.Users.AddRange(user1, user2, user3, user4, user5);
                

                // Insert dummy data for download
                var download1 = new Download { DownloadDate = new DateTime(2023, 11, 22), ProjectDocumentId = projectDocument1.ProjectDocumentId, UserId = user1.UserId };
                var download2 = new Download { DownloadDate = new DateTime(2023, 12, 08), ProjectDocumentId = projectDocument2.ProjectDocumentId, UserId = user2.UserId };
                var download3 = new Download { DownloadDate = new DateTime(2023, 05, 24), ProjectDocumentId = projectDocument3.ProjectDocumentId, UserId = user3.UserId };
                var download4 = new Download { DownloadDate = new DateTime(2023, 07, 23), ProjectDocumentId = projectDocument4.ProjectDocumentId, UserId = user4.UserId };
                var download5 = new Download { DownloadDate = new DateTime(2023, 09, 18), ProjectDocumentId = projectDocument5.ProjectDocumentId, UserId = user5.UserId };

                _dbContext.Downloads.AddRange(download1, download2, download3, download4, download5);

                
                _dbContext.SaveChanges();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
                
        }

        public void DeleteAllData()
        {
            Console.WriteLine("Inside Delete Data Function ");
            try
            {
                // Delete all data from the Graduates table
               

                // Delete all data from the ProjectDocuments table
                var allProjectDocuments = _dbContext.ProjectDocuments.ToList();
                _dbContext.ProjectDocuments.RemoveRange(allProjectDocuments);

                // Delete all data from the Users table
                var allUsers = _dbContext.Users.ToList();
                _dbContext.Users.RemoveRange(allUsers);

                // Delete all data from the Downloads table
                var allDownloads = _dbContext.Downloads.ToList();
                _dbContext.Downloads.RemoveRange(allDownloads);
                /*
                var allGraduates = _dbContext.Graduates.ToList();
                Console.WriteLine($"Graduates List:  {allGraduates}");
                _dbContext.Graduates.RemoveRange(allGraduates);
                */
                // Save changes to the database
                _dbContext.SaveChanges();

              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }


        public ViewResult StudentsWithPublishedDocuments()
        {

            try
            {
                var students = _dbContext.Graduates.ToList();
                return View(students);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                return View();
            }
        }

        public IActionResult StudentsWithPublishedDocumentsByResearchTopic(string researchTopic)
        {
            try
            {


                var students = _dbContext.Graduates
                    .Where(g => g.ProjectDocuments.Any(pd => pd.ResearchTopic == researchTopic))
                    .ToList();

                return View(students);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                return View();
            }

        }

        public IActionResult DownloadedDocumentsByUserAndDate(int userId, DateTime date)
        {
            var documents = _dbContext.Downloads
                .Include(d => d.ProjectDocument)
                .Include(d => d.Graduate)
                .Include(d => d.User)
                .Where(d => d.UserId == userId && d.DownloadDate == date)
                .Select(d => new
                {
                    Title = d.ProjectDocument.Title,
                    ResearchTopic = d.ProjectDocument.ResearchTopic,
                    UserName = d.User.UserName,
                    DownloadDate = d.DownloadDate
                })
                .ToList();

            return View(documents);
        }

        public IActionResult TopResearchTopicsByPublishedDocuments()
        {
            var researchTopicsWithCount = _dbContext.ProjectDocuments
                .GroupBy(pd => pd.ResearchTopic)
                .Select(g => new ProjectDocument
                {
                    ResearchTopic = g.Key,
                    DownloadsCount = g.Count()
                })
                .ToList();

            return View(researchTopicsWithCount);
        }

        public IActionResult TopResearchTopicsByDownloads()
        {
            var researchTopicsWithCount = _dbContext.ProjectDocuments
                .GroupBy(pd => pd.ResearchTopic)
                .Select(g => new ProjectDocument
                {
                    ResearchTopic = g.Key,
                    DownloadsCount = g.Count()
                })
                .ToList();

            return View(researchTopicsWithCount);
        }

    }
}

