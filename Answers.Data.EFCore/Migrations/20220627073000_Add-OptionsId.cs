using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Answers.Data.Migrations
{
    public partial class AddOptionsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SurveyAnswer",
                newName: "UserPersonalityId");

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyOptionId",
                table: "Survey",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SurveyPersonOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequireFirstName = table.Column<bool>(type: "bit", nullable: false),
                    RequireSecondName = table.Column<bool>(type: "bit", nullable: false),
                    RequireGender = table.Column<bool>(type: "bit", nullable: false),
                    RequireAges = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyPersonOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyPersonOptions_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyPersonOptions_SurveyId",
                table: "SurveyPersonOptions",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyPersonOptions");

            migrationBuilder.DropColumn(
                name: "SurveyOptionId",
                table: "Survey");

            migrationBuilder.RenameColumn(
                name: "UserPersonalityId",
                table: "SurveyAnswer",
                newName: "UserId");
        }
    }
}
