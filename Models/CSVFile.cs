using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace SurveyTool.Models
{
    public class CSVFile
    {
        public int ID { get; set; }
        public string Tag { get; set; }
        public string Prompt { get; set; }
        public string Required { get; set; }
        public string Dependency { get; set; }
        public string Order { get; set; }
        public string Type { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Response { get; set; }
        [Name("Min Label")]
        public string MinLabel { get; set; }
        [Name("Max Label")]
        public string MaxLabel { get; set; }
        [Name("Response Options")]
        public string ResponseOptions { get; set; }
    }

}
