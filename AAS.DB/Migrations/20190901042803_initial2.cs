using Microsoft.EntityFrameworkCore.Migrations;

namespace AAS.DB.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRN",
                table: "Period");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CRN",
                table: "Period",
                nullable: false,
                defaultValue: 0);
        }
    }
}
