using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.EntityFramework.Core.Migrations.OrderDb
{
    /// <inheritdoc />
    public partial class OrderTableAddRiderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "order_RiderId",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_RiderId",
                table: "sys_Order");
        }
    }
}
