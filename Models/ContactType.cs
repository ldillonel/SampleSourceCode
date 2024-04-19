using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            Feedback = new HashSet<Feedback>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ContactTypeName { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
