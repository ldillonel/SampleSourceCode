using System;
using System.ComponentModel.DataAnnotations;
using SurveyTool.Enumerations;

namespace SurveyTool.Extensions
{
    public static class ScaleResponseEnumExtensions
    {
        public static string ToDisplayName(this ScaleResponseEnum enumElement, string failureText)
        {
            var enumFieldInfo = enumElement.GetType().GetField(enumElement.ToString());
            
            return Attribute.IsDefined(enumFieldInfo, typeof(DisplayAttribute))
                ? (Attribute.GetCustomAttribute(enumFieldInfo, typeof(DisplayAttribute)) as DisplayAttribute).GetName()
                : failureText;
        }
    }
}
