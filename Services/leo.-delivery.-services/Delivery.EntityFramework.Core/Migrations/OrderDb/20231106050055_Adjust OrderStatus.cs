using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.EntityFramework.Core.Migrations.OrderDb
{
    /// <inheritdoc />
    public partial class AdjustOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order_Status",
                table: "sys_Order",
                newName: "order_StatusKey");

            migrationBuilder.AddColumn<string>(
                name: "order_StatusValue",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_StatusValue",
                table: "sys_Order");

            migrationBuilder.RenameColumn(
                name: "order_StatusKey",
                table: "sys_Order",
                newName: "order_Status");
        }
    }
}
