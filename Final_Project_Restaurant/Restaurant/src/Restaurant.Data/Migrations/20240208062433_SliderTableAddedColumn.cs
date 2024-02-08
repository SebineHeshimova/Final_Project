using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class SliderTableAddedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAbout",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlog",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGallery",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMenu",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReservation",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShop",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAbout",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsBlog",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsGallery",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsMenu",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsReservation",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsShop",
                table: "Sliders");
        }
    }
}
