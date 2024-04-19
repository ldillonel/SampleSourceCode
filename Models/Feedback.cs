using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class Feedback
    {
        public Feedback()

        {
            ResultResponse = new HashSet<ResultResponse>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionSetId { get; set; }
        public int ContactTypeId { get; set; }
        public Guid? RespondentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateTimeCreated { get; set; }
        public DateTime? TimeCompleted { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual QuestionSet QuestionSet { get; set; }
        public virtual Respondent Respondent { get; set; }
        public virtual ICollection<ResultResponse> ResultResponse { get; set; }
    }
}
