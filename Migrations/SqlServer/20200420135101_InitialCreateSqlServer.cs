using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyTool.Migrations.SqlServer
{
    public partial class InitialCreateSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact_Type",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact_Type_Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Type", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Question_Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Category_Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Introduction = table.Column<string>(unicode: false, nullable: false),
                    Version_Number = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    DateTime_Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Response_Type",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Response_Type_Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response_Type", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Survey_Type",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Survey_Type_Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: false),
                    DateTime_Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey_Type", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_Text = table.Column<string>(unicode: false, nullable: false),
                    QuestionSetID = table.Column<int>(nullable: false),
                    Question_Category_ID = table.Column<int>(nullable: false),
                    Response_Type_ID = table.Column<int>(nullable: false),
                    Ordinal_Position = table.Column<int>(nullable: false),
                    Min = table.Column<int>(nullable: true),
                    Max = table.Column<int>(nullable: true),
                    Page = table.Column<int>(nullable: false),
                    Page_Order = table.Column<int>(nullable: false),
                    Dependency_Question_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.ID);
                    table.ForeignKey(
                        name: "Question_Question_Category",
                        column: x => x.Question_Category_ID,
                        principalTable: "Question_Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Question_QuestionSet",
                        column: x => x.QuestionSetID,
                        principalTable: "QuestionSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Question_Response_Type",
                        column: x => x.Response_Type_ID,
                        principalTable: "Response_Type",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Respondent",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MOS = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    First_Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    Last_Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    Role_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respondent", x => x.ID);
                    table.ForeignKey(
                        name: "Respondent_Role",
                        column: x => x.Role_ID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Access_Code = table.Column<string>(maxLength: 10, nullable: true),
                    SurveyName = table.Column<string>(nullable: true),
                    Survey_Type_ID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: false),
                    Start_DateTime = table.Column<DateTime>(nullable: false),
                    End_DateTime = table.Column<DateTime>(nullable: false),
                    DateTime_Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.ID);
                    table.ForeignKey(
                        name: "Survey_Survey_Type",
                        column: x => x.Survey_Type_ID,
                        principalTable: "Survey_Type",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_ID = table.Column<int>(nullable: false),
                    Response_Text = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    Ordinal_Position = table.Column<int>(nullable: false),
                    ResponseValue = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.ID);
                    table.ForeignKey(
                        name: "Response_Question",
                        column: x => x.Question_ID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    QuestionSetID = table.Column<int>(nullable: false),
                    Contact_Type_ID = table.Column<int>(nullable: false),
                    Respondent_ID = table.Column<Guid>(nullable: true),
                    DateTime_Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    time_completed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.ID);
                    table.ForeignKey(
                        name: "actual_QuestionSet_Contact_Type",
                        column: x => x.Contact_Type_ID,
                        principalTable: "Contact_Type",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "actual_QuestionSet_QuestionSet",
                        column: x => x.QuestionSetID,
                        principalTable: "QuestionSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "actual_QuestionSet_Respondent",
                        column: x => x.Respondent_ID,
                        principalTable: "Respondent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Related_Survey",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyID = table.Column<int>(nullable: false),
                    Related_Survey_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Related_Survey", x => x.ID);
                    table.ForeignKey(
                        name: "Related_Survey_Related_Survey",
                        column: x => x.Related_Survey_ID,
                        principalTable: "Survey",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Related_Survey_Survey",
                        column: x => x.SurveyID,
                        principalTable: "Survey",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionSet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyID = table.Column<int>(nullable: false),
                    QuestionSetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionSet", x => x.ID);
                    table.ForeignKey(
                        name: "SurveyQuestionSet_QuestionSet",
                        column: x => x.QuestionSetID,
                        principalTable: "QuestionSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SurveyQuestionSet_Survey",
                        column: x => x.SurveyID,
                        principalTable: "Survey",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Result_Response",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Feedback_ID = table.Column<Guid>(nullable: false),
                    Question_ID = table.Column<int>(nullable: false),
                    Response_ID = table.Column<int>(nullable: true),
                    Response_Text = table.Column<string>(unicode: false, nullable: true),
                    Version_Number = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    DateTime_Created = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result_Response", x => x.ID);
                    table.ForeignKey(
                        name: "actual_Response_actual_QuestionSet",
                        column: x => x.Feedback_ID,
                        principalTable: "Feedback",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "actual_Response_Question",
                        column: x => x.Question_ID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "actual_Response_Response",
                        column: x => x.Response_ID,
                        principalTable: "Response",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Survey_Type",
                columns: new[] { "ID", "Description", "Survey_Type_Name" },
                values: new object[] { 1, "ATEC Survey", "ATEC" });

            migrationBuilder.InsertData(
                table: "Survey_Type",
                columns: new[] { "ID", "Description", "Survey_Type_Name" },
                values: new object[] { 2, "ITN Survey", "ITN" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Contact_Type_AK_1",
                table: "Contact_Type",
                column: "Contact_Type_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Contact_Type_ID",
                table: "Feedback",
                column: "Contact_Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_QuestionSetID",
                table: "Feedback",
                column: "QuestionSetID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Respondent_ID",
                table: "Feedback",
                column: "Respondent_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Question_Category_ID",
                table: "Question",
                column: "Question_Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Response_Type_ID",
                table: "Question",
                column: "Response_Type_ID");

            migrationBuilder.CreateIndex(
                name: "Question_AK_1",
                table: "Question",
                columns: new[] { "QuestionSetID", "Ordinal_Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Question_Category_AK_1",
                table: "Question_Category",
                column: "Question_Category_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Related_Survey_Related_Survey_ID",
                table: "Related_Survey",
                column: "Related_Survey_ID");

            migrationBuilder.CreateIndex(
                name: "Related_Survey_AK_1",
                table: "Related_Survey",
                columns: new[] { "SurveyID", "Related_Survey_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Respondent_AK_1",
                table: "Respondent",
                column: "Email",
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Respondent_Role_ID",
                table: "Respondent",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "Response_AK_2",
                table: "Response",
                columns: new[] { "Question_ID", "Ordinal_Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Response_Type_AK_1",
                table: "Response_Type",
                column: "Response_Type_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Result_Response_Feedback_ID",
                table: "Result_Response",
                column: "Feedback_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Result_Response_Question_ID",
                table: "Result_Response",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Result_Response_Response_ID",
                table: "Result_Response",
                column: "Response_ID");

            migrationBuilder.CreateIndex(
                name: "Role_AK_1",
                table: "Role",
                column: "Role_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_Survey_Type_ID",
                table: "Survey",
                column: "Survey_Type_ID");

            migrationBuilder.CreateIndex(
                name: "Survey_Type_AK_1",
                table: "Survey_Type",
                column: "Survey_Type_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionSet_QuestionSetID",
                table: "SurveyQuestionSet",
                column: "QuestionSetID");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionSet_SurveyID",
                table: "SurveyQuestionSet",
                column: "SurveyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Related_Survey");

            migrationBuilder.DropTable(
                name: "Result_Response");

            migrationBuilder.DropTable(
                name: "SurveyQuestionSet");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropTable(
                name: "Contact_Type");

            migrationBuilder.DropTable(
                name: "Respondent");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Survey_Type");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Question_Category");

            migrationBuilder.DropTable(
                name: "QuestionSet");

            migrationBuilder.DropTable(
                name: "Response_Type");
        }
    }
}
