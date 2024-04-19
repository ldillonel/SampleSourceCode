using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace SurveyTool.Database
{
    public class SurveyToolSqliteDbContext : SurveyToolDbContextBase
    {
        public SurveyToolSqliteDbContext(DbContextOptions<SurveyToolSqliteDbContext> options, IHostEnvironment env) : base(options, env)
        {
        }
    }
}
