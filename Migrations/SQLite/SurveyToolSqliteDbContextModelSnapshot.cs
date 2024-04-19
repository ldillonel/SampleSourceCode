﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyTool.Database;

namespace SurveyTool.Migrations.SQLite
{
    [DbContext(typeof(SurveyToolSqliteDbContext))]
    partial class SurveyToolSqliteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SurveyTool.Models.ContactType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactTypeName")
                        .IsRequired()
                        .HasColumnName("Contact_Type_Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("ContactTypeName")
                        .IsUnique()
                        .HasName("Contact_Type_AK_1");

                    b.ToTable("Contact_Type");
                });

            modelBuilder.Entity("SurveyTool.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("TEXT");

                    b.Property<int>("ContactTypeId")
                        .HasColumnName("Contact_Type_ID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("QuestionSetId")
                        .HasColumnName("QuestionSetID")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("RespondentId")
                        .HasColumnName("Respondent_ID")
                        .HasColumnType("TEXT");

                    b.Property<int>("SurveyId")
                        .HasColumnName("Survey_ID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("TimeCompleted")
                        .HasColumnName("time_completed")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContactTypeId");

                    b.HasIndex("QuestionSetId");

                    b.HasIndex("RespondentId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("SurveyTool.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DependencyQuestionId")
                        .HasColumnName("Dependency_Question_ID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Max")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Min")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrdinalPosition")
                        .HasColumnName("Ordinal_Position")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Page")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PageOrder")
                        .HasColumnName("Page_Order")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionCategoryId")
                        .HasColumnName("Question_Category_ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionSetId")
                        .HasColumnName("QuestionSetID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnName("Question_Text")
                        .HasColumnType("TEXT")
                        .IsUnicode(false);

                    b.Property<int>("ResponseTypeId")
                        .HasColumnName("Response_Type_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionCategoryId");

                    b.HasIndex("ResponseTypeId");

                    b.HasIndex("QuestionSetId", "OrdinalPosition")
                        .IsUnique()
                        .HasName("Question_AK_1");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("SurveyTool.Models.QuestionCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionCategoryName")
                        .IsRequired()
                        .HasColumnName("Question_Category_Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("QuestionCategoryName")
                        .IsUnique()
                        .HasName("Question_Category_AK_1");

                    b.ToTable("Question_Category");
                });

            modelBuilder.Entity("SurveyTool.Models.QuestionSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .IsUnicode(false);

                    b.Property<string>("QuestionSetName")
                        .IsRequired()
                        .HasColumnName("QuestionSet_Name")
                        .HasColumnType("TEXT")
                        .IsUnicode(false);

                    b.Property<string>("VersionNumber")
                        .IsRequired()
                        .HasColumnName("Version_Number")
                        .HasColumnType("TEXT")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("QuestionSet");
                });

            modelBuilder.Entity("SurveyTool.Models.RelatedSurvey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelatedSurveyId")
                        .HasColumnName("Related_Survey_ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SurveyId")
                        .HasColumnName("SurveyID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RelatedSurveyId");

                    b.HasIndex("SurveyId", "RelatedSurveyId")
                        .IsUnique()
                        .HasName("Related_Survey_AK_1");

                    b.ToTable("Related_Survey");
                });

            modelBuilder.Entity("SurveyTool.Models.Respondent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .HasColumnName("First_Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .HasColumnName("Last_Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("Mos")
                        .HasColumnName("MOS")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<int>("RoleId")
                        .HasColumnName("Role_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Respondent");
                });

            modelBuilder.Entity("SurveyTool.Models.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrdinalPosition")
                        .HasColumnName("Ordinal_Position")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnName("Question_ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ResponseText")
                        .IsRequired()
                        .HasColumnName("Response_Text")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<int?>("ResponseValue")
                        .HasColumnName("ResponseValue")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId", "OrdinalPosition")
                        .IsUnique()
                        .HasName("Response_AK_2");

                    b.ToTable("Response");
                });

            modelBuilder.Entity("SurveyTool.Models.ResponseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ResponseTypeName")
                        .IsRequired()
                        .HasColumnName("Response_Type_Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("ResponseTypeName")
                        .IsUnique()
                        .HasName("Response_Type_AK_1");

                    b.ToTable("Response_Type");
                });

            modelBuilder.Entity("SurveyTool.Models.ResultResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid>("FeedbackId")
                        .HasColumnName("Feedback_ID")
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionId")
                        .HasColumnName("Question_ID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ResponseId")
                        .HasColumnName("Response_ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ResponseText")
                        .HasColumnName("Response_Text")
                        .HasColumnType("TEXT")
                        .IsUnicode(false);

                    b.Property<string>("VersionNumber")
                        .IsRequired()
                        .HasColumnName("Version_Number")
                        .HasColumnType("TEXT")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("ResponseId");

                    b.ToTable("Result_Response");
                });

            modelBuilder.Entity("SurveyTool.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .IsUnicode(false);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnName("Role_Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("RoleName")
                        .IsUnique()
                        .HasName("Role_AK_1");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("SurveyTool.Models.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .IsUnicode(false);

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnName("End_DateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnName("Start_DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("SurveyAccessCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Access_Code")
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("SurveyName")
                        .HasColumnName("Survey_Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("SurveyTypeId")
                        .HasColumnName("Survey_Type_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SurveyTypeId");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("SurveyTool.Models.SurveyQuestionSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionSetId")
                        .HasColumnName("QuestionSetID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SurveyId")
                        .HasColumnName("SurveyID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionSetId");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestionSet");
                });

            modelBuilder.Entity("SurveyTool.Models.SurveyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .IsUnicode(false);

                    b.Property<string>("SurveyTypeName")
                        .IsRequired()
                        .HasColumnName("Survey_Type_Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("SurveyTypeName")
                        .IsUnique()
                        .HasName("Survey_Type_AK_1");

                    b.ToTable("Survey_Type");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateTimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "ATEC Survey",
                            SurveyTypeName = "ATEC"
                        },
                        new
                        {
                            Id = 2,
                            DateTimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "ITN Survey",
                            SurveyTypeName = "ITN"
                        });
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyTool.Models.Feedback", b =>
                {
                    b.HasOne("SurveyTool.Models.ContactType", "ContactType")
                        .WithMany("Feedback")
                        .HasForeignKey("ContactTypeId")
                        .HasConstraintName("actual_QuestionSet_Contact_Type")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.QuestionSet", "QuestionSet")
                        .WithMany("Feedback")
                        .HasForeignKey("QuestionSetId")
                        .HasConstraintName("actual_QuestionSet_QuestionSet")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.Respondent", "Respondent")
                        .WithMany("Feedback")
                        .HasForeignKey("RespondentId")
                        .HasConstraintName("actual_QuestionSet_Respondent");

                    b.HasOne("SurveyTool.Models.Survey", "Survey")
                        .WithMany("Feedback")
                        .HasForeignKey("SurveyId")
                        .HasConstraintName("Feedback_Survey")
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyTool.Models.Question", b =>
                {
                    b.HasOne("SurveyTool.Models.QuestionCategory", "QuestionCategory")
                        .WithMany("Question")
                        .HasForeignKey("QuestionCategoryId")
                        .HasConstraintName("Question_Question_Category")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.QuestionSet", "QuestionSet")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionSetId")
                        .HasConstraintName("Question_QuestionSet")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.ResponseType", "ResponseType")
                        .WithMany("Question")
                        .HasForeignKey("ResponseTypeId")
                        .HasConstraintName("Question_Response_Type")
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyTool.Models.RelatedSurvey", b =>
                {
                    b.HasOne("SurveyTool.Models.Survey", "RelatedSurveyNavigation")
                        .WithMany("RelatedSurveyRelatedSurveyNavigation")
                        .HasForeignKey("RelatedSurveyId")
                        .HasConstraintName("Related_Survey_Related_Survey")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.Survey", "Survey")
                        .WithMany("RelatedSurveySurvey")
                        .HasForeignKey("SurveyId")
                        .HasConstraintName("Related_Survey_Survey")
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyTool.Models.Respondent", b =>
                {
                    b.HasOne("SurveyTool.Models.Role", "Role")
                        .WithMany("Respondent")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("Respondent_Role")
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyTool.Models.Response", b =>
                {
                    b.HasOne("SurveyTool.Models.Question", "Question")
                        .WithMany("Response")
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("Response_Question")
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyTool.Models.ResultResponse", b =>
                {
                    b.HasOne("SurveyTool.Models.Feedback", "Feedback")
                        .WithMany("ResultResponse")
                        .HasForeignKey("FeedbackId")
                        .HasConstraintName("actual_Response_actual_QuestionSet")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.Question", "Question")
                        .WithMany("ResultResponse")
                        .HasForeignKey("QuestionId")
                        .HasConstraintName("actual_Response_Question")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.Response", "Response")
                        .WithMany("ResultResponse")
                        .HasForeignKey("ResponseId")
                        .HasConstraintName("actual_Response_Response");
                });

            modelBuilder.Entity("SurveyTool.Models.Survey", b =>
                {
                    b.HasOne("SurveyTool.Models.SurveyType", "SurveyType")
                        .WithMany("Survey")
                        .HasForeignKey("SurveyTypeId")
                        .HasConstraintName("Survey_Survey_Type")
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyTool.Models.SurveyQuestionSet", b =>
                {
                    b.HasOne("SurveyTool.Models.QuestionSet", "QuestionSet")
                        .WithMany("SurveyQuestionSets")
                        .HasForeignKey("QuestionSetId")
                        .HasConstraintName("SurveyQuestionSet_QuestionSet")
                        .IsRequired();

                    b.HasOne("SurveyTool.Models.Survey", "Survey")
                        .WithMany("SurveyQuestionSet")
                        .HasForeignKey("SurveyId")
                        .HasConstraintName("SurveyQuestionSet_Survey")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
