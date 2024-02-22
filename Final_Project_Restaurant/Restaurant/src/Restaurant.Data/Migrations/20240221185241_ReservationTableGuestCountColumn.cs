using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class ReservationTableGuestCountColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "GuestCount",
                table: "Reservations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestCount",
                table: "Reservations");
        }
    }
}
