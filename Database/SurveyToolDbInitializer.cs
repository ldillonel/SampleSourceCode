using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurveyTool.Enumerations;
using SurveyTool.Extensions;
using SurveyTool.Models;

namespace SurveyTool.Database
{
    public class SurveyToolDbInitializer
    {
        public static void Initialize(SurveyToolDbContextBase context, UserManager<User> userManager)
        {
            /* 
                NOTE: EF Core's Migrate method will not create migrations automatically. 
                You must run 
                    dotnet ef migrations add --context <contextName> 
                before starting the application if there are breaking changes to the model.
            */
            context.Database.Migrate();

            using (var seedTransaction = context.Database.BeginTransaction())
            {
                if (!context.Role.Any())
                {
                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Role ON");
                    }

                    context.Role.Add(new Role
                    {
                        Id = 1,
                        RoleName = "Test Role",
                        Description = "This is a test role for development purposes."
                    });

                    context.SaveChanges();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Role OFF");
                    }
                }

                if (!context.Users.Any())
                {
                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT AspNetRoles ON");
                    }
                    // Add the Admin role to the database
                    int rowsChanged = context.Database.ExecuteSqlRaw(
                        "INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES ('E0571EF4-ADD3-49F4-9DCC-212C69B8AB1B', 'Admin', 'ADMIN', NULL)");
                    //Full SQL command:
                    //"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE NormalizedName = 'ADMIN') BEGIN INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES ('E0571EF4-ADD3-49F4-9DCC-212C69B8AB1B', 'Admin', 'ADMIN', NULL) END");

                    // Create seed user
                    User user = new User
                    {
                        UserName = "admin",
                        Token = "",
                        Role = "admin"
                    };
                    // Seed user with manual password to initialize
                    IdentityResult result = userManager.CreateAsync(user, "Admin123!").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "admin").Wait();
                    }

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT AspNetRoles OFF");
                    }
                }

                if (!context.ContactType.Any())
                {
                    context.ContactType.AddRange(new List<ContactType>
                    {
                        new ContactType{
                            ContactTypeName = "Email"
                        },
                        new ContactType
                        {
                            ContactTypeName = "Phone"
                        }
                    });
                }


                if (!context.QuestionCategory.Any())
                {
                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Question_Category ON");
                    }

                    context.QuestionCategory.AddRange(new List<QuestionCategory>
                    {
                        new QuestionCategory
                        {
                            Id = (int)QuestionCategoryEnum.Demographic,
                            QuestionCategoryName = "Demographic"
                        },
                        new QuestionCategory
                        {
                            Id = (int)QuestionCategoryEnum.Equipment,
                            QuestionCategoryName = "Equipment"
                        },
                        new QuestionCategory
                        {
                            Id = (int)QuestionCategoryEnum.Sentiment,
                            QuestionCategoryName = "Sentiment"
                        }
                    });

                    context.SaveChanges();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Question_Category OFF");
                    }
                }

                if (!context.ResponseType.Any())
                {
                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Response_Type ON");
                    }

                    context.ResponseType.AddRange(new List<ResponseType>
                    {
                        new ResponseType
                        {
                            Id = (int)ResponseTypeEnum.SingleSelect,
                            ResponseTypeName = "Single Select"
                        },
                        new ResponseType
                        {
                            Id = (int)ResponseTypeEnum.MultiSelect,
                            ResponseTypeName = "Multi Select"
                        },
                        new ResponseType
                        {
                            Id = (int)ResponseTypeEnum.Binary,
                            ResponseTypeName = "Binary"
                        },
                        new ResponseType
                        {
                            Id = (int)ResponseTypeEnum.Scale,
                            ResponseTypeName = "Scale"
                        },
                        new ResponseType
                        {
                            Id = (int)ResponseTypeEnum.Rating,
                            ResponseTypeName = "Rating"
                        },
                        new ResponseType
                        {
                            Id = (int)ResponseTypeEnum.SingleText,
                            ResponseTypeName = "Single Text"
                        },
                        new ResponseType
                        {
                            Id = (int)ResponseTypeEnum.MultiText,
                            ResponseTypeName = "Multi Text"
                        }
                    });

                    context.SaveChanges();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Response_Type OFF");
                    }
                }

                context.SaveChanges();
                seedTransaction.Commit();
            }
        }
    }
}