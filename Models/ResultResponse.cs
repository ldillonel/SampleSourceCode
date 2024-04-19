using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class ResultResponse
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid FeedbackId { get; set; }
        public int QuestionId { get; set; }
        public int? ResponseId { get; set; }
        public string ResponseText { get; set; }
        public string VersionNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateTimeCreated { get; set; }

        public virtual Feedback Feedback { get; set; }
        public virtual Question Question { get; set; }
        public virtual Response Response { get; set; }
    }
}
