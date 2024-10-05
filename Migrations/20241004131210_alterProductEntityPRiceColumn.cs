using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class alterProductEntityPRiceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pPrice",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pPrice",
                table: "Products");
        }
    }
}
