using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.EntityFramework.Core.Migrations.OrderDb
{
    /// <inheritdoc />
    public partial class AdjuestItemVolumeAndItemRemarkType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "order_ItemVolume",
                table: "sys_Order",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "order_ItemVolume",
                table: "sys_Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
