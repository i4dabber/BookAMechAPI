using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookAMech.Data.Migrations
{
    public partial class ReservationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<int>(nullable: false),
                    Phonenumber = table.Column<int>(nullable: false),
                    startDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
