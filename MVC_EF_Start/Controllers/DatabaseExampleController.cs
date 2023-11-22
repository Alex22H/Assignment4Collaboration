using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.Controllers
{
    public class DatabaseExampleController : Controller
    {
        public ApplicationDbContext dbContext;

        public DatabaseExampleController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> DatabaseOperations()
        {
            // CREATE operation
            // Insert dummy data for student
            var student1 = new Student { StudentFirstName = "John", StudentLastName = "Doe" };
            var student2 = new Student { StudentFirstName = "Jane", StudentLastName = "Smith" };
            var student3 = new Student { StudentFirstName = "Bob", StudentLastName = "Johnson" };
            var student4 = new Student { StudentFirstName = "Alice", StudentLastName = "Williams" };
            var student5 = new Student { StudentFirstName = "Charlie", StudentLastName = "Brown" };

            dbContext.Students.AddRange(student1, student2, student3, student4, student5);

            // Insert dummy data for Document
            var document1 = new Document { Title = "Research Paper 1", ResearchTopic = "Data Science", PublishedDate = new DateTime(2022, 01, 01), StudentID = student1.StudentID };
            var document2 = new Document { Title = "Thesis 1", ResearchTopic = "Artificial Intelligence", PublishedDate = new DateTime(2022, 01, 01), StudentID = student2.StudentID };
            var document3 = new Document { Title = "Case Study 1", ResearchTopic = "Data Science", PublishedDate = new DateTime(2022, 01, 01), StudentID = student3.StudentID };
            var document4 = new Document { Title = "Project Report 1", ResearchTopic = "Artificial Intelligence", PublishedDate = new DateTime(2022, 01, 01), StudentID = student4.StudentID };
            var document5 = new Document { Title = "Experiment Results", ResearchTopic = "Data Science", PublishedDate = new DateTime(2022, 01, 01), StudentID = student5.StudentID };

            dbContext.Documents.AddRange(document1, document2, document3, document4, document5);

            // Insert dummy data for User
            var user1 = new User { UserFirstName = "David", UserLastName = "Lee" };
            var user2 = new User { UserFirstName = "Emily", UserLastName = "Clark" };
            var user3 = new User { UserFirstName = "Michael", UserLastName = "Wang" };
            var user4 = new User { UserFirstName = "Sophia", UserLastName = "Garcia" };
            var user5 = new User { UserFirstName = "Ethan", UserLastName = "Taylor" };

            dbContext.Users.AddRange(user1, user2, user3, user4, user5);

            // Insert dummy data for Download
            var download1 = new Download { DownloadDate = new DateTime(2022, 11, 22), DocumentID = document1.DocumentID, UserID = user1.UserID };
            var download2 = new Download { DownloadDate = new DateTime(2022, 12, 08), DocumentID = document2.DocumentID, UserID = user2.UserID };
            var download3 = new Download { DownloadDate = new DateTime(2022, 05, 24), DocumentID = document3.DocumentID, UserID = user3.UserID };
            var download4 = new Download { DownloadDate = new DateTime(2022, 07, 23), DocumentID = document4.DocumentID, UserID = user4.UserID };
            var download5 = new Download { DownloadDate = new DateTime(2022, 09, 18), DocumentID = document5.DocumentID, UserID = user5.UserID };

            dbContext.Downloads.AddRange(download1, download2, download3, download4, download5);

            dbContext.SaveChanges();

            await dbContext.SaveChangesAsync();

            return View();
        }

        public IActionResult StudentsWithPublishedDocuments()
        {
            var publishedStudents = dbContext.Documents
                .Select(sd => new 
                {
                    FirstName = sd.Student.StudentFirstName,
                    LastName = sd.Student.StudentLastName
                })
                .ToList();

            return View(publishedStudents);
        }



        /*
        public IActionResult pubdocs()
        {
            return View(); // Display a form to enter the research topic
        }

        [HttpPost]
        public IActionResult pubdocs(string researchTopic)
        {
            if (string.IsNullOrEmpty(researchTopic))
            {
                ViewData["ErrorMessage"] = "Please enter a research topic.";
                return View("pubdocs");
            }

            var studentDocuments = dbContext.Documents
                .Where(d => d.ResearchTopic == researchTopic)
                .Select(sd => new
                {
                    FirstName = sd.Student.StudentFirstName,
                    LastName = sd.Student.StudentLastName
                })
                .ToList();

            return View(studentDocuments);
        }
        */

    }
}