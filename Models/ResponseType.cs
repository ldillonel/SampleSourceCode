using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class ResponseType
    {
        public ResponseType()
        {
            Question = new HashSet<Question>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ResponseTypeName { get; set; }

        public virtual ICollection<Question> Question { get; set; }
    }
}
