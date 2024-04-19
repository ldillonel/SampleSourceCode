using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class QuestionSet
    {
        public QuestionSet()
        {
            Feedback = new HashSet<Feedback>();
            Questions = new HashSet<Question>();
            SurveyQuestionSets = new HashSet<SurveyQuestionSet>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string QuestionSetName { get; set; }
        public string Introduction { get; set; }
        public string VersionNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateTimeCreated { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<SurveyQuestionSet> SurveyQuestionSets { get; set; }
    }
}
