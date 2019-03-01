using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetcoresample.Migrations
{
    public partial class AddScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Customers",
                nullable: true);

            migrationBuilder.Sql("UPDATE dbo.Customers SET Score = Age");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE dbo.Customers SET Age = Score");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Customers");
        }
    }
}
