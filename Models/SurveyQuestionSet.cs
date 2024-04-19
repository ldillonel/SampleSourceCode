using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class SurveyQuestionSet
    {
        public SurveyQuestionSet()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionSetId { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual QuestionSet QuestionSet { get; set; }
    }
}
