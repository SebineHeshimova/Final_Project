using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class ReservationTableAppUserNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_AppUserId",
                table: "Reservations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
