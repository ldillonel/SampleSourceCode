using System;
using System.Collections.Generic;

namespace SurveyTool.Models
{
    public partial class RelatedSurvey
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int RelatedSurveyId { get; set; }

        public virtual Survey RelatedSurveyNavigation { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
