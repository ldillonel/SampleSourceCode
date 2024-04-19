using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace SurveyTool.Models
{
    public partial class Survey
    {
        public Survey()
        {
            SurveyQuestionSet = new HashSet<SurveyQuestionSet>();
            RelatedSurveyRelatedSurveyNavigation = new HashSet<RelatedSurvey>();
            RelatedSurveySurvey = new HashSet<RelatedSurvey>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [StringLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SurveyAccessCode { get; set; }
        public string SurveyName { get; set; }
        public int SurveyTypeId { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateTimeCreated { get; set; }

        public virtual SurveyType SurveyType { get; set; }
        public virtual ICollection<SurveyQuestionSet> SurveyQuestionSet { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<RelatedSurvey> RelatedSurveyRelatedSurveyNavigation { get; set; }
        public virtual ICollection<RelatedSurvey> RelatedSurveySurvey { get; set; }
    }
}
