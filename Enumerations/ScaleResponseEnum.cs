using System.ComponentModel.DataAnnotations;

namespace SurveyTool.Enumerations
{
    public enum ScaleResponseEnum
    {
        [Display(Name = "Strongly Disagree")]
        StronglyDisagree = 1,
        Disagree = 2,
        Agree = 3,
        [Display(Name = "Strongly Agree")]
        StronglyAgree = 4
    }
}
