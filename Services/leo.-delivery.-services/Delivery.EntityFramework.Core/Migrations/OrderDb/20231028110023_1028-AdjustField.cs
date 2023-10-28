using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.EntityFramework.Core.Migrations.OrderDb
{
    /// <inheritdoc />
    public partial class _1028AdjustField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order_GoodsWidth",
                table: "sys_Order",
                newName: "order_ItemWidth");

            migrationBuilder.RenameColumn(
                name: "order_GoodsWeight",
                table: "sys_Order",
                newName: "order_ItemWeight");

            migrationBuilder.RenameColumn(
                name: "order_GoodsType",
                table: "sys_Order",
                newName: "order_PayMethond");

            migrationBuilder.RenameColumn(
                name: "order_GoodsPrice",
                table: "sys_Order",
                newName: "order_ItemPrice");

            migrationBuilder.RenameColumn(
                name: "order_GoodsName",
                table: "sys_Order",
                newName: "order_ItemType");

            migrationBuilder.RenameColumn(
                name: "order_GoodsLong",
                table: "sys_Order",
                newName: "order_ItemLong");

            migrationBuilder.RenameColumn(
                name: "order_GoodsHight",
                table: "sys_Order",
                newName: "order_ItemRemark");

            migrationBuilder.RenameColumn(
                name: "order_GoodsDelivery",
                table: "sys_Order",
                newName: "order_Fee");

            migrationBuilder.RenameColumn(
                name: "order_GoddsNums",
                table: "sys_Order",
                newName: "order_TransactionId");

            migrationBuilder.RenameColumn(
                name: "order_CreateDeptName",
                table: "sys_Order",
                newName: "order_ItemName");

            migrationBuilder.RenameColumn(
                name: "order_CreateDeptId",
                table: "sys_Order",
                newName: "order_ItemHight");

            migrationBuilder.AddColumn<string>(
                name: "order_BeginAddress",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "order_Charge",
                table: "sys_Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "order_EndAddress",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "order_ImgUrls",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "order_IsReceived",
                table: "sys_Order",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "order_ItemQuantity",
                table: "sys_Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "order_ItemVolume",
                table: "sys_Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "order_Reference",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "order_SenderPersonId",
                table: "sys_Order",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_BeginAddress",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_Charge",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_EndAddress",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_ImgUrls",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_IsReceived",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_ItemQuantity",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_ItemVolume",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_Reference",
                table: "sys_Order");

            migrationBuilder.DropColumn(
                name: "order_SenderPersonId",
                table: "sys_Order");

            migrationBuilder.RenameColumn(
                name: "order_TransactionId",
                table: "sys_Order",
                newName: "order_GoddsNums");

            migrationBuilder.RenameColumn(
                name: "order_PayMethond",
                table: "sys_Order",
                newName: "order_GoodsType");

            migrationBuilder.RenameColumn(
                name: "order_ItemWidth",
                table: "sys_Order",
                newName: "order_GoodsWidth");

            migrationBuilder.RenameColumn(
                name: "order_ItemWeight",
                table: "sys_Order",
                newName: "order_GoodsWeight");

            migrationBuilder.RenameColumn(
                name: "order_ItemType",
                table: "sys_Order",
                newName: "order_GoodsName");

            migrationBuilder.RenameColumn(
                name: "order_ItemRemark",
                table: "sys_Order",
                newName: "order_GoodsHight");

            migrationBuilder.RenameColumn(
                name: "order_ItemPrice",
                table: "sys_Order",
                newName: "order_GoodsPrice");

            migrationBuilder.RenameColumn(
                name: "order_ItemName",
                table: "sys_Order",
                newName: "order_CreateDeptName");

            migrationBuilder.RenameColumn(
                name: "order_ItemLong",
                table: "sys_Order",
                newName: "order_GoodsLong");

            migrationBuilder.RenameColumn(
                name: "order_ItemHight",
                table: "sys_Order",
                newName: "order_CreateDeptId");

            migrationBuilder.RenameColumn(
                name: "order_Fee",
                table: "sys_Order",
                newName: "order_GoodsDelivery");
        }
    }
}
