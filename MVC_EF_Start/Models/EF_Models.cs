using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_EF_Start.Models
{
    public class Graduate
    {
        public int GraduateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ResearchTopic { get; set; }

        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
    }

    public class ProjectDocument
    {
        public int ProjectDocumentId { get; set; }
        public int GraduateId { get; set; }
        public string Title { get; set; }
        public string ResearchTopic { get; set; }
        public DateTime PublishedDate { get; set; }

        public int DownloadsCount { get; set; }
    
    }

    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        
    }

    public class Download
    {
        public int DownloadId { get; set; }
        public int ProjectDocumentId { get; set; }
        public int UserId { get; set; }
        public DateTime DownloadDate { get; set; }
        public virtual Graduate Graduate { get; set; }

        public virtual ProjectDocument ProjectDocument { get; set; }
        public virtual User User { get; set; }
    }
}
