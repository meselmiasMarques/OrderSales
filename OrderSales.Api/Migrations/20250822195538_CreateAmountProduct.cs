using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSales.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateAmountProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Product");
        }
    }
}
