using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace SurveyTool.Database
{
    public class SurveyToolSqlServerDbContext : SurveyToolDbContextBase
    {
        public SurveyToolSqlServerDbContext(DbContextOptions<SurveyToolSqlServerDbContext> options, IHostEnvironment env) : base(options, env)
        {
        }
    }
}
