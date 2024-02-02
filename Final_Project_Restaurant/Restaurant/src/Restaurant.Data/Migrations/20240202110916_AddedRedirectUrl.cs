using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class AddedRedirectUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedirectAction",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "RedirectController",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "RedirectAction",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "RedirectController",
                table: "Banners");

            migrationBuilder.AddColumn<string>(
                name: "RedirectUrl",
                table: "Sliders",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RedirectUrl",
                table: "Banners",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedirectUrl",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "RedirectUrl",
                table: "Banners");

            migrationBuilder.AddColumn<string>(
                name: "RedirectAction",
                table: "Sliders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RedirectController",
                table: "Sliders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RedirectAction",
                table: "Banners",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RedirectController",
                table: "Banners",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
