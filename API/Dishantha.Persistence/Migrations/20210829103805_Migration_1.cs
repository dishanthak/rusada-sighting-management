using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dishantha.Persistence.Migrations
{
    public partial class Migration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sightings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Registration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AircraftPhotoName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<int>(type: "int", nullable: true),
                    UpdatedUser = table.Column<int>(type: "int", nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sightings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Sightings",
                columns: new[] { "Id", "AircraftPhotoName", "CreatedDateTime", "CreatedUser", "DateAndTime", "Location", "Make", "Model", "Registration", "UpdatedDateTime", "UpdatedUser" },
                values: new object[,]
                {
                    { 1, "b56b02aa-385d-42f2-8809-5f8d80c9ae71.png", new DateTimeOffset(new DateTime(2021, 8, 29, 16, 8, 5, 178, DateTimeKind.Unspecified).AddTicks(8180), new TimeSpan(0, 5, 30, 0, 0)), null, new DateTime(2010, 8, 28, 8, 20, 0, 0, DateTimeKind.Unspecified), "London Gatwick", "AD Scout", "777-300ER", "G-RNAC", null, null },
                    { 2, "38dc45b2-0126-4657-9869-a1f8860b274f.png", new DateTimeOffset(new DateTime(2021, 8, 29, 16, 8, 5, 178, DateTimeKind.Unspecified).AddTicks(9665), new TimeSpan(0, 5, 30, 0, 0)), null, new DateTime(2010, 8, 28, 8, 25, 0, 0, DateTimeKind.Unspecified), "London Gatwick", "Aerfer Ariete", "777-302ER", "G-RNAC", null, null },
                    { 3, "bf98f2a3-d7c6-45c6-9481-a9ee844075f5.png", new DateTimeOffset(new DateTime(2021, 8, 29, 16, 8, 5, 178, DateTimeKind.Unspecified).AddTicks(9671), new TimeSpan(0, 5, 30, 0, 0)), null, new DateTime(2010, 8, 28, 8, 30, 0, 0, DateTimeKind.Unspecified), "London Gatwick", "Beardmore W.B.V", "777-303ER", "G-RNAC", null, null },
                    { 4, "40ec6d2f-3bf3-4225-9c14-1d7b99096129.png", new DateTimeOffset(new DateTime(2021, 8, 29, 16, 8, 5, 178, DateTimeKind.Unspecified).AddTicks(9678), new TimeSpan(0, 5, 30, 0, 0)), null, new DateTime(2010, 8, 28, 8, 40, 0, 0, DateTimeKind.Unspecified), "London Gatwick", "Dewoitine D.25", "777-304ER", "G-RNAC", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sightings");
        }
    }
}
