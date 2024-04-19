using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class Question
    {
        public Question()
        {
            Response = new HashSet<Response>();
            ResultResponse = new HashSet<ResultResponse>();
        }
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionCategoryId { get; set; }
        public int ResponseTypeId { get; set; }
        public int OrdinalPosition { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public int Page { get; set; }
        public int PageOrder { get; set; }
        public int? DependencyQuestionId { get; set; }

        public virtual QuestionSet QuestionSet { get; set; }
        public virtual QuestionCategory QuestionCategory { get; set; }
        public virtual ResponseType ResponseType { get; set; }
        public virtual ICollection<Response> Response { get; set; }
        public virtual ICollection<ResultResponse> ResultResponse { get; set; }
    }
}
