using Microsoft.EntityFrameworkCore.Migrations;

namespace NIS.DALCore.Migrations
{
    public partial class deparmentid_toWorkFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "WorkFlow",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "WorkFlow");
        }
    }
}
