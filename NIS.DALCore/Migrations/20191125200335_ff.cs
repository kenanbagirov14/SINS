using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIS.DALCore.Migrations
{
    public partial class ff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerRequestArchive",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerRequestTypeId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CustomerNumber = table.Column<int>(nullable: true),
                    ContactNumber = table.Column<string>(maxLength: 50, nullable: true),
                    ContractNumber = table.Column<string>(maxLength: 50, nullable: true),
                    AON = table.Column<string>(maxLength: 50, nullable: true),
                    Text = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SourceTypeId = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    RequestStatusId = table.Column<int>(nullable: true),
                    RatingId = table.Column<int>(nullable: true),
                    MailUniqueId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRequestArchive", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerRequestArchive");
        }
    }
}
