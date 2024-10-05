using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class alterProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "pName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pName",
                table: "Products",
                newName: "Name");
        }
    }
}
