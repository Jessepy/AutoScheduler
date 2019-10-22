using Microsoft.EntityFrameworkCore.Migrations;

namespace AAS.DB.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRN",
                table: "Exam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CRN",
                table: "Exam",
                nullable: false,
                defaultValue: 0);
        }
    }
}
