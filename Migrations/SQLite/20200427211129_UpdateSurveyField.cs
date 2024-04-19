using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyTool.Migrations.SQLite
{
    public partial class UpdateSurveyField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Respondent_AK_1",
                table: "Respondent");

            migrationBuilder.RenameColumn(
                name: "SurveyName",
                table: "Survey",
                newName: "Survey_Name");

            migrationBuilder.AddColumn<string>(
                name: "QuestionSet_Name",
                table: "QuestionSet",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Survey_ID",
                table: "Feedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Survey_ID",
                table: "Feedback",
                column: "Survey_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Feedback_Survey",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_Survey_ID",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "QuestionSet_Name",
                table: "QuestionSet");

            migrationBuilder.DropColumn(
                name: "Survey_ID",
                table: "Feedback");

            migrationBuilder.RenameColumn(
                name: "Survey_Name",
                table: "Survey",
                newName: "SurveyName");

            migrationBuilder.CreateIndex(
                name: "Respondent_AK_1",
                table: "Respondent",
                column: "Email");
        }
    }
}
