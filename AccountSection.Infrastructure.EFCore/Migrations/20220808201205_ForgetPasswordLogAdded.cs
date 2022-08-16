using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountSection.Infrastructure.EFCore.Migrations
{
    public partial class ForgetPasswordLogAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForgetPasswordLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgetPasswordLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForgetPasswordLogs");
        }
    }
}
