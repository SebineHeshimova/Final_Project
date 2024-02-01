﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Data.Migrations
{
    public partial class AddedRedirectController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RedirectCotroller",
                table: "Banners",
                newName: "RedirectController");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RedirectController",
                table: "Banners",
                newName: "RedirectCotroller");
        }
    }
}
