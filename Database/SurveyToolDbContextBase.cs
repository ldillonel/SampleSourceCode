using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SurveyTool.Enumerations;
using SurveyTool.Models;

namespace SurveyTool.Database {
    public class SurveyToolDbContextBase : IdentityDbContext<User> {
        private IHostEnvironment env;

        protected SurveyToolDbContextBase (DbContextOptions options, IHostEnvironment env) : base (options) {
            this.env = env;
        }

        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionSet> QuestionSet { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategory { get; set; }
        public virtual DbSet<SurveyQuestionSet> SurveyQuestionSets { get; set; }
        public virtual DbSet<RelatedSurvey> RelatedSurvey { get; set; }
        public virtual DbSet<Respondent> Respondent { get; set; }
        public virtual DbSet<Response> Response { get; set; }
        public virtual DbSet<ResponseType> ResponseType { get; set; }
        public virtual DbSet<ResultResponse> ResultResponse { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SurveyType> SurveyType { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);

            modelBuilder.Entity<ContactType> (entity => {
                entity.ToTable ("Contact_Type");

                entity.HasIndex (e => e.ContactTypeName)
                    .HasName ("Contact_Type_AK_1")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.ContactTypeName)
                    .IsRequired ()
                    .HasColumnName ("Contact_Type_Name")
                    .HasMaxLength (128)
                    .IsUnicode (false);
            });

            modelBuilder.Entity<Feedback> (entity => {
                entity.ToTable ("Feedback");

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.SurveyId).HasColumnName ("Survey_ID");

                entity.Property (e => e.ContactTypeId).HasColumnName ("Contact_Type_ID");

                entity.Property (e => e.DateTimeCreated).HasColumnName ("DateTime_Created").HasDefaultValueSql ("CURRENT_TIMESTAMP");

                entity.Property (e => e.QuestionSetId).HasColumnName ("QuestionSetID");

                entity.Property (e => e.RespondentId).HasColumnName ("Respondent_ID");

                entity.Property (e => e.TimeCompleted).HasColumnName ("time_completed");

                entity.HasOne (d => d.Survey)
                    .WithMany (p => p.Feedback)
                    .HasForeignKey (d => d.SurveyId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Feedback_Survey");

                entity.HasOne (d => d.ContactType)
                    .WithMany (p => p.Feedback)
                    .HasForeignKey (d => d.ContactTypeId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("actual_QuestionSet_Contact_Type");

                entity.HasOne (d => d.QuestionSet)
                    .WithMany (p => p.Feedback)
                    .HasForeignKey (d => d.QuestionSetId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("actual_QuestionSet_QuestionSet");

                entity.HasOne (d => d.Respondent)
                    .WithMany (p => p.Feedback)
                    .HasForeignKey (d => d.RespondentId)
                    .HasConstraintName ("actual_QuestionSet_Respondent");
            });

            modelBuilder.Entity<Question> (entity => {
                entity.ToTable ("Question");

                entity.HasIndex (e => new { e.QuestionSetId, e.OrdinalPosition })
                    .HasName ("Question_AK_1")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.DependencyQuestionId).HasColumnName ("Dependency_Question_ID");

                entity.Property (e => e.OrdinalPosition).HasColumnName ("Ordinal_Position");

                entity.Property (e => e.PageOrder).HasColumnName ("Page_Order");

                entity.Property (e => e.QuestionSetId).HasColumnName ("QuestionSetID");

                entity.Property (e => e.QuestionText)
                    .IsRequired ()
                    .HasColumnName ("Question_Text")
                    .IsUnicode (false);

                entity.Property (e => e.QuestionCategoryId).HasColumnName ("Question_Category_ID");

                entity.Property (e => e.ResponseTypeId).HasColumnName ("Response_Type_ID");

                entity.HasOne (d => d.QuestionSet)
                    .WithMany (p => p.Questions)
                    .HasForeignKey (d => d.QuestionSetId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Question_QuestionSet");

                entity.HasOne (d => d.QuestionCategory)
                    .WithMany (p => p.Question)
                    .HasForeignKey (d => d.QuestionCategoryId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Question_Question_Category");

                entity.HasOne (d => d.ResponseType)
                    .WithMany (p => p.Question)
                    .HasForeignKey (d => d.ResponseTypeId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Question_Response_Type");
            });

            modelBuilder.Entity<QuestionSet> (entity => {
                entity.ToTable ("QuestionSet");

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.DateTimeCreated).HasColumnName ("DateTime_Created").HasDefaultValueSql ("CURRENT_TIMESTAMP");

                entity.Property (e => e.QuestionSetName)
                    .IsRequired ()
                    .HasColumnName ("QuestionSet_Name")
                    .IsUnicode (false);

                entity.Property (e => e.Introduction)
                    .IsRequired ()
                    .IsUnicode (false);

                entity.Property (e => e.VersionNumber)
                    .IsRequired ()
                    .HasColumnName ("Version_Number")
                    .HasMaxLength (25)
                    .IsUnicode (false);

            });

            modelBuilder.Entity<QuestionCategory> (entity => {
                entity.ToTable ("Question_Category");

                entity.HasIndex (e => e.QuestionCategoryName)
                    .HasName ("Question_Category_AK_1")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.QuestionCategoryName)
                    .IsRequired ()
                    .HasColumnName ("Question_Category_Name")
                    .HasMaxLength (128)
                    .IsUnicode (false);
            });

            modelBuilder.Entity<RelatedSurvey> (entity => {
                entity.ToTable ("Related_Survey");

                entity.HasIndex (e => new { e.SurveyId, e.RelatedSurveyId })
                    .HasName ("Related_Survey_AK_1")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.RelatedSurveyId).HasColumnName ("Related_Survey_ID");

                entity.Property (e => e.SurveyId).HasColumnName ("SurveyID");

                entity.HasOne (d => d.RelatedSurveyNavigation)
                    .WithMany (p => p.RelatedSurveyRelatedSurveyNavigation)
                    .HasForeignKey (d => d.RelatedSurveyId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Related_Survey_Related_Survey");

                entity.HasOne (d => d.Survey)
                    .WithMany (p => p.RelatedSurveySurvey)
                    .HasForeignKey (d => d.SurveyId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Related_Survey_Survey");
            });

            modelBuilder.Entity<Respondent> (entity => {
                entity.ToTable ("Respondent");

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.Email)
                    .HasMaxLength (128)
                    .IsUnicode (false);

                entity.Property (e => e.FirstName)
                    .HasColumnName ("First_Name")
                    .HasMaxLength (128)
                    .IsUnicode (false);

                entity.Property (e => e.LastName)
                    .HasColumnName ("Last_Name")
                    .HasMaxLength (128)
                    .IsUnicode (false);

                entity.Property (e => e.Mos)
                    .HasColumnName ("MOS")
                    .HasMaxLength (128)
                    .IsUnicode (false);

                entity.Property (e => e.RoleId).HasColumnName ("Role_ID");

                entity.HasOne (d => d.Role)
                    .WithMany (p => p.Respondent)
                    .HasForeignKey (d => d.RoleId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Respondent_Role");
            });

            modelBuilder.Entity<Response> (entity => {
                entity.ToTable ("Response");

                entity.HasIndex (e => new { e.QuestionId, e.OrdinalPosition })
                    .HasName ("Response_AK_2")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.OrdinalPosition).HasColumnName ("Ordinal_Position");

                entity.Property (e => e.QuestionId).HasColumnName ("Question_ID");

                entity.Property (e => e.ResponseValue).HasColumnName ("ResponseValue");

                entity.Property (e => e.ResponseText)
                    .IsRequired ()
                    .HasColumnName ("Response_Text")
                    .HasMaxLength (128)
                    .IsUnicode (false);

                entity.HasOne (d => d.Question)
                    .WithMany (p => p.Response)
                    .HasForeignKey (d => d.QuestionId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Response_Question");
            });

            modelBuilder.Entity<ResponseType> (entity => {
                entity.ToTable ("Response_Type");

                entity.HasIndex (e => e.ResponseTypeName)
                    .HasName ("Response_Type_AK_1")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.ResponseTypeName)
                    .IsRequired ()
                    .HasColumnName ("Response_Type_Name")
                    .HasMaxLength (128)
                    .IsUnicode (false);
            });

            modelBuilder.Entity<ResultResponse> (entity => {
                entity.ToTable ("Result_Response");

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.DateTimeCreated).HasColumnName ("DateTime_Created").HasDefaultValueSql ("CURRENT_TIMESTAMP");

                entity.Property (e => e.FeedbackId).HasColumnName ("Feedback_ID");

                entity.Property (e => e.QuestionId).HasColumnName ("Question_ID");

                entity.Property (e => e.ResponseId).HasColumnName ("Response_ID");

                entity.Property (e => e.ResponseText)
                    .HasColumnName ("Response_Text")
                    .IsUnicode (false);

                entity.Property (e => e.VersionNumber)
                    .IsRequired ()
                    .HasColumnName ("Version_Number")
                    .HasMaxLength (25)
                    .IsUnicode (false);

                entity.HasOne (d => d.Feedback)
                    .WithMany (p => p.ResultResponse)
                    .HasForeignKey (d => d.FeedbackId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("actual_Response_actual_QuestionSet");

                entity.HasOne (d => d.Question)
                    .WithMany (p => p.ResultResponse)
                    .HasForeignKey (d => d.QuestionId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("actual_Response_Question");

                entity.HasOne (d => d.Response)
                    .WithMany (p => p.ResultResponse)
                    .HasForeignKey (d => d.ResponseId)
                    .HasConstraintName ("actual_Response_Response");
            });

            modelBuilder.Entity<Role> (entity => {
                entity.ToTable ("Role");

                entity.HasIndex (e => e.RoleName)
                    .HasName ("Role_AK_1")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.Description)
                    .IsRequired ()
                    .IsUnicode (false);

                entity.Property (e => e.RoleName)
                    .IsRequired ()
                    .HasColumnName ("Role_Name")
                    .HasMaxLength (128)
                    .IsUnicode (false);
            });

            modelBuilder.Entity<Survey> (entity => {
                entity.ToTable ("Survey");

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.SurveyName).HasColumnName ("Survey_Name");

                entity.Property (e => e.SurveyAccessCode).HasColumnName ("Access_Code");

                entity.Property (e => e.DateTimeCreated).HasColumnName ("DateTime_Created").HasDefaultValueSql ("CURRENT_TIMESTAMP");

                entity.Property (e => e.Description)
                    .IsRequired ()
                    .IsUnicode (false);

                entity.Property (e => e.EndDateTime).HasColumnName ("End_DateTime");

                entity.Property (e => e.StartDateTime).HasColumnName ("Start_DateTime");

                entity.Property (e => e.SurveyTypeId).HasColumnName ("Survey_Type_ID");

                entity.HasOne (d => d.SurveyType)
                    .WithMany (p => p.Survey)
                    .HasForeignKey (d => d.SurveyTypeId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("Survey_Survey_Type");
            });

            modelBuilder.Entity<SurveyType> (entity => {
                entity.ToTable ("Survey_Type");

                entity.HasIndex (e => e.SurveyTypeName)
                    .HasName ("Survey_Type_AK_1")
                    .IsUnique ();

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.DateTimeCreated).HasColumnName ("DateTime_Created").HasDefaultValueSql ("CURRENT_TIMESTAMP");

                entity.Property (e => e.Description)
                    .IsRequired ()
                    .IsUnicode (false);

                entity.Property (e => e.SurveyTypeName)
                    .IsRequired ()
                    .HasColumnName ("Survey_Type_Name")
                    .HasMaxLength (128)
                    .IsUnicode (false);
            });

            modelBuilder.Entity<SurveyQuestionSet> (entity => {
                entity.ToTable ("SurveyQuestionSet");

                entity.Property (e => e.Id).HasColumnName ("ID");

                entity.Property (e => e.SurveyId).HasColumnName ("SurveyID");

                entity.Property (e => e.QuestionSetId).HasColumnName ("QuestionSetID");

                entity.HasOne (d => d.Survey)
                    .WithMany (p => p.SurveyQuestionSet)
                    .HasForeignKey (d => d.SurveyId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("SurveyQuestionSet_Survey");

                entity.HasOne (d => d.QuestionSet)
                    .WithMany (p => p.SurveyQuestionSets)
                    .HasForeignKey (d => d.QuestionSetId)
                    .OnDelete (DeleteBehavior.ClientSetNull)
                    .HasConstraintName ("SurveyQuestionSet_QuestionSet");
            });

            modelBuilder.Entity<SurveyType> ().HasData (
                new SurveyType { Id = 1, Description = "ATEC Survey", SurveyTypeName = "ATEC" },
                new SurveyType { Id = 2, Description = "ITN Survey", SurveyTypeName = "ITN" }
            );

//TODO: Revisit this to see why the following data is not being set in the DB. 
            if (this.env.IsDevelopment ()) {
                //place development-only seed data within statements like this, using HasData.
                Console.WriteLine ("I am in Development environment.");
                modelBuilder.Entity<Survey> ().HasData (
                    new Survey {
                        Id = 1,
                            SurveyAccessCode = "test",
                            SurveyName = "Test Survey",
                            SurveyTypeId = 1,
                            Description = "This is a seeded test survey.",
                            StartDateTime = new DateTime (2020, 2, 28).ToUniversalTime (),
                            EndDateTime = new DateTime (2020, 3, 31).ToUniversalTime ()
                    }
                );

                modelBuilder.Entity<QuestionSet> ().HasData (
                    new QuestionSet {
                        Id = 1,
                            QuestionSetName = "TestQS",
                            VersionNumber = "0.1.0",
                            Introduction = "This is a seeded test question set."
                    }
                );

                modelBuilder.Entity<SurveyQuestionSet> ().HasData (
                    new SurveyQuestionSet {
                        Id = 1,
                            QuestionSetId = 1,
                            SurveyId = 1
                    }
                );

                modelBuilder.Entity<QuestionCategory> ().HasData (
                    new QuestionCategory {

                        Id = (int) QuestionCategoryEnum.Demographic,
                            QuestionCategoryName = "Demographic"
                    },
                    new QuestionCategory {
                        Id = (int) QuestionCategoryEnum.Equipment,
                            QuestionCategoryName = "Equipment"
                    },
                    new QuestionCategory {
                        Id = (int) QuestionCategoryEnum.Sentiment,
                            QuestionCategoryName = "Sentiment"
                    }
                );

                modelBuilder.Entity<Question> ().HasData (
                    new Question {
                        Id = 1,
                            QuestionSetId = 1,
                            OrdinalPosition = 1,
                            QuestionText = "This is a sample single text question.",
                            ResponseTypeId = (int) ResponseTypeEnum.SingleText,
                            QuestionCategoryId = (int) QuestionCategoryEnum.Demographic,

                    },
                    new Question {
                        Id = 2,
                            QuestionSetId = 1,
                            OrdinalPosition = 2,
                            QuestionText = "This is a sample binary (true/false; yes/no) question.",
                            ResponseTypeId = (int) ResponseTypeEnum.Binary,
                            QuestionCategoryId = (int) QuestionCategoryEnum.Sentiment
                    },
                    new Question {
                        Id = 3,
                            QuestionSetId = 1,
                            OrdinalPosition = 3,
                            QuestionText = "This is a sample single select question.",
                            ResponseTypeId = (int) ResponseTypeEnum.SingleSelect,
                            QuestionCategoryId = (int) QuestionCategoryEnum.Equipment
                    },
                    new Question {
                        Id = 4,
                            QuestionSetId = 1,
                            OrdinalPosition = 4,
                            QuestionText = "This is a sample default scale question.",
                            ResponseTypeId = (int) ResponseTypeEnum.Scale,
                            QuestionCategoryId = (int) QuestionCategoryEnum.Sentiment,
                            Max = 4,
                            Min = 1
                    },
                    new Question {
                        Id = 5,
                            QuestionSetId = 1,
                            OrdinalPosition = 5,
                            QuestionText = "This is a sample multi select question.",
                            ResponseTypeId = (int) ResponseTypeEnum.MultiSelect,
                            QuestionCategoryId = (int) QuestionCategoryEnum.Demographic
                    },
                    new Question {
                        Id = 6,
                            QuestionSetId = 1,
                            OrdinalPosition = 6,
                            QuestionText = "This is a sample multi text question.",
                            ResponseTypeId = (int) ResponseTypeEnum.MultiText,
                            QuestionCategoryId = (int) QuestionCategoryEnum.Demographic,

                    },
                    new Question {
                        Id = 7,
                            QuestionSetId = 1,
                            OrdinalPosition = 7,
                            QuestionText = "This is a sample rating question with custom icons and labels.",
                            ResponseTypeId = (int) ResponseTypeEnum.Rating,
                            QuestionCategoryId = (int) QuestionCategoryEnum.Sentiment,
                            Max = 10,
                            Min = 1
                    }
                );

                modelBuilder.Entity<Response> ().HasData (
                    new Response {
                        Id = 1,
                            QuestionId = 3,
                            OrdinalPosition = 1,
                            ResponseText = "Single Option 1",
                            ResponseValue = 1
                    },
                    new Response {
                        Id = 2,
                            QuestionId = 3,
                            OrdinalPosition = 2,
                            ResponseText = "Single Option 2",
                            ResponseValue = 2
                    },
                    new Response {
                        Id = 3,
                            QuestionId = 3,
                            OrdinalPosition = 3,
                            ResponseText = "Single Option 3",
                            ResponseValue = 3
                    },
                    new Response {
                        Id = 4,
                            QuestionId = 5,
                            OrdinalPosition = 1,
                            ResponseText = "Multi Option 1",
                            ResponseValue = 1
                    },
                    new Response {
                        Id = 5,
                            QuestionId = 5,
                            OrdinalPosition = 2,
                            ResponseText = "Multi Option 2",
                            ResponseValue = 2
                    },
                    new Response {
                        Id = 6,
                            QuestionId = 5,
                            OrdinalPosition = 3,
                            ResponseText = "Multi Option 3",
                            ResponseValue = 3
                    }
                );

            }

        }
    }
}