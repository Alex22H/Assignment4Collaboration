using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace MVC_EF_Start.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public List<Download> Downloads { get; set; }
    }

    public class Download
    {
        public string DownloadID { get; set; }
        public DateTime DownloadDate { get; set; }
        public string UserID { get; set; }
        public string DocumentID { get; set; }

        public User User { get; set; }
        public Document Document { get; set; }
    }

    public class Student
    {
        public string StudentID { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public List<Document> Documents { get; set; }
    }

    public class Document
    {
        public string DocumentID { get; set; }
        public string Title { get; set; }
        public string ResearchTopic { get; set; }
        public DateTime PublishedDate { get; set; }
        public string StudentID { get; set; }

        public Student Student { get; set; }
        public List<Download> Downloads { get; set; }
    }

}