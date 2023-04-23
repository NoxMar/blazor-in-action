using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingTrails.Api.Migrations
{
    public partial class AddOwnerToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Trails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Trails");
        }
    }
}
