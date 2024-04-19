using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class Response
    {
        public Response()
        {
            ResultResponse = new HashSet<ResultResponse>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string ResponseText { get; set; }
        public int OrdinalPosition { get; set; }
        public int? ResponseValue { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<ResultResponse> ResultResponse { get; set; }
    }
}
