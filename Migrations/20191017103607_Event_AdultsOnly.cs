using Microsoft.EntityFrameworkCore.Migrations;

namespace EventApp.Migrations
{
    public partial class Event_AdultsOnly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdultsOnly",
                table: "Events",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultsOnly",
                table: "Events");
        }
    }
}
