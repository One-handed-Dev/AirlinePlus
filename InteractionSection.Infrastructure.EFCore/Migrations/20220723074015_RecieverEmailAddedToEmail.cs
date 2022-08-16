using Microsoft.EntityFrameworkCore.Migrations;

namespace InteractionSection.Infrastructure.EFCore.Migrations
{
    public partial class RecieverEmailAddedToEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reciever",
                table: "Emails",
                newName: "RecieverName");

            migrationBuilder.AddColumn<string>(
                name: "RecieverEmail",
                table: "Emails",
                type: "nvarchar(511)",
                maxLength: 511,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieverEmail",
                table: "Emails");

            migrationBuilder.RenameColumn(
                name: "RecieverName",
                table: "Emails",
                newName: "Reciever");
        }
    }
}
