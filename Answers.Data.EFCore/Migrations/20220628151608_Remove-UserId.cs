using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Answers.Data.Migrations
{
    public partial class RemoveUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPersonalityId",
                table: "SurveyAnswer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserPersonalityId",
                table: "SurveyAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
