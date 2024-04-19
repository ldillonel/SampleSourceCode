using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyTool.Models
{
    public partial class Role
    {
        public Role()
        {
            Respondent = new HashSet<Respondent>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Respondent> Respondent { get; set; }
    }
}
