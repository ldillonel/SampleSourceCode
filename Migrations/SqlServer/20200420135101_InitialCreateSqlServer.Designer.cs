﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyTool.Database;

namespace SurveyTool.Migrations.SqlServer
{
    [DbContext(typeof(SurveyToolSqlServerDbContext))]
    [Migration("20200420135101_InitialCreateSqlServer")]
    partial class InitialCreateSqlServer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SurveyTool.Models.ContactType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactTypeName")
                        .IsRequired()
                        .HasColumnName("Contact_Type_Name")
                        .HasColumnType("varchar(128)")
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
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ContactTypeId")
                        .HasColumnName("Contact_Type_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("QuestionSetId")
                        .HasColumnName("QuestionSetID")
                        .HasColumnType("int");

                    b.Property<Guid?>("RespondentId")
                        .HasColumnName("Respondent_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("TimeCompleted")
                        .HasColumnName("time_completed")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContactTypeId");

                    b.HasIndex("QuestionSetId");

                    b.HasIndex("RespondentId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("SurveyTool.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DependencyQuestionId")
                        .HasColumnName("Dependency_Question_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Max")
                        .HasColumnType("int");

                    b.Property<int?>("Min")
                        .HasColumnType("int");

                    b.Property<int>("OrdinalPosition")
                        .HasColumnName("Ordinal_Position")
                        .HasColumnType("int");

                    b.Property<int>("Page")
                        .HasColumnType("int");

                    b.Property<int>("PageOrder")
                        .HasColumnName("Page_Order")
                        .HasColumnType("int");

                    b.Property<int>("QuestionCategoryId")
                        .HasColumnName("Question_Category_ID")
                        .HasColumnType("int");

                    b.Property<int>("QuestionSetId")
                        .HasColumnName("QuestionSetID")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnName("Question_Text")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<int>("ResponseTypeId")
                        .HasColumnName("Response_Type_ID")
                        .HasColumnType("int");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("QuestionCategoryName")
                        .IsRequired()
                        .HasColumnName("Question_Category_Name")
                        .HasColumnType("varchar(128)")
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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("VersionNumber")
                        .IsRequired()
                        .HasColumnName("Version_Number")
                        .HasColumnType("varchar(25)")
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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RelatedSurveyId")
                        .HasColumnName("Related_Survey_ID")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnName("SurveyID")
                        .HasColumnType("int");

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
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .HasColumnName("First_Name")
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .HasColumnName("Last_Name")
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("Mos")
                        .HasColumnName("MOS")
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<int>("RoleId")
                        .HasColumnName("Role_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .HasName("Respondent_AK_1")
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Respondent");
                });

            modelBuilder.Entity("SurveyTool.Models.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrdinalPosition")
                        .HasColumnName("Ordinal_Position")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnName("Question_ID")
                        .HasColumnType("int");

                    b.Property<string>("ResponseText")
                        .IsRequired()
                        .HasColumnName("Response_Text")
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<int?>("ResponseValue")
                        .HasColumnName("ResponseValue")
                        .HasColumnType("int");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ResponseTypeName")
                        .IsRequired()
                        .HasColumnName("Response_Type_Name")
                        .HasColumnType("varchar(128)")
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
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid>("FeedbackId")
                        .HasColumnName("Feedback_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("QuestionId")
                        .HasColumnName("Question_ID")
                        .HasColumnType("int");

                    b.Property<int?>("ResponseId")
                        .HasColumnName("Response_ID")
                        .HasColumnType("int");

                    b.Property<string>("ResponseText")
                        .HasColumnName("Response_Text")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("VersionNumber")
                        .IsRequired()
                        .HasColumnName("Version_Number")
                        .HasColumnType("varchar(25)")
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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnName("Role_Name")
                        .HasColumnType("varchar(128)")
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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnName("End_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnName("Start_DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SurveyAccessCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Access_Code")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("SurveyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurveyTypeId")
                        .HasColumnName("Survey_Type_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyTypeId");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("SurveyTool.Models.SurveyQuestionSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("QuestionSetId")
                        .HasColumnName("QuestionSetID")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnName("SurveyID")
                        .HasColumnType("int");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DateTime_Created")
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("SurveyTypeName")
                        .IsRequired()
                        .HasColumnName("Survey_Type_Name")
                        .HasColumnType("varchar(128)")
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

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