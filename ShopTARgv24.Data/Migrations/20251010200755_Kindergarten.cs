using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARgv24.Data.Migrations
{
    /// <inheritdoc />
    public partial class Kindergarten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RealEstateId",
                table: "FileToDatabases",
                newName: "KindergartenId");

            migrationBuilder.CreateTable(
                name: "Kindergartens",
                columns: table => new
                {
                    KindergartenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildrenCount = table.Column<int>(type: "int", nullable: true),
                    KindergartenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kindergartens", x => x.KindergartenId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kindergartens");

            migrationBuilder.RenameColumn(
                name: "KindergartenId",
                table: "FileToDatabases",
                newName: "RealEstateId");
        }
    }
}
