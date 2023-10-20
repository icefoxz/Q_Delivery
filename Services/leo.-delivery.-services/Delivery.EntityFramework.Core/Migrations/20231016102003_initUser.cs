using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.EntityFramework.Core.Migrations
{
    /// <inheritdoc />
    public partial class initUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_Order_Tag");

            migrationBuilder.DropColumn(
                name: "menu_Type",
                table: "sys_Menu");

            migrationBuilder.DropColumn(
                name: "dept_Level",
                table: "sys_Dept");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_Id",
                table: "sys_User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "limit_Id",
                table: "sys_User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_Id",
                table: "sys_User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_User",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isEnable",
                table: "sys_User",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isEnable",
                table: "sys_Tag",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "per_UserPwd",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "per_UserName",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "per_Type",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "per_NormalizedPhone",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_Id",
                table: "sys_Person",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Person",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "job_Id",
                table: "sys_Person",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "order_StateId",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "order_EndPlaceId",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "order_EndLng",
                table: "sys_Order",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "order_EndLat",
                table: "sys_Order",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "order_BenginPlaceId",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "menu_Path",
                table: "sys_Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Menu",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isOuterJoin",
                table: "sys_Menu",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "menu_ComponentName",
                table: "sys_Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "menu_FileName",
                table: "sys_Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "menu_Icon",
                table: "sys_Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "menu_Link",
                table: "sys_Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "log_Type",
                table: "sys_Log",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "log_OptUser",
                table: "sys_Log",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "log_OptIP",
                table: "sys_Log",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "log_Message",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "log_ApiMethod",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "log_ApiPath",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "log_Browser",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "log_BrowserInfo",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "log_Device",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "log_ElapsedMilliseconds",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "log_OptPort",
                table: "sys_Log",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "log_Os",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "log_Params",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Limit_Menu",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "menu_IdArray",
                table: "sys_Limit_Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Limit",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_Id",
                table: "sys_Job",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Job",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dict_JsonValue",
                table: "sys_Dict",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "sys_Dict",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dict_Name",
                table: "sys_Dict",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isSystemBuilt",
                table: "sys_Dict",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Dept",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "sys_File",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_File", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Lingau",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lingau_Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    person_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Lingau", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Lingau_OptLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    opt_User = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    opt_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opt_BeUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    opt_BeginAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    opt_EndAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Lingau_OptLog", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Order_TagOrReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_TagOrReport_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    order_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tagManager_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Order_TagOrReport", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_sys_Order_TagOrReport_sys_Order_order_Id",
                        column: x => x.order_Id,
                        principalTable: "sys_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sys_Tag_Manager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Tag_Manager", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_sys_Tag_Manager_sys_Tag_tag_Id",
                        column: x => x.tag_Id,
                        principalTable: "sys_Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Person_job_Id",
                table: "sys_Person",
                column: "job_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_File_del_Status",
                table: "sys_File",
                column: "del_Status");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Lingau_person_Id_del_Status",
                table: "sys_Lingau",
                columns: new[] { "person_Id", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Lingau_OptLog_opt_User_opt_BeUser_del_Status",
                table: "sys_Lingau_OptLog",
                columns: new[] { "opt_User", "opt_BeUser", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_TagOrReport_order_Id",
                table: "sys_Order_TagOrReport",
                column: "order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_TagOrReport_order_TagOrReport_Name_order_Id_del_Status",
                table: "sys_Order_TagOrReport",
                columns: new[] { "order_TagOrReport_Name", "order_Id", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Tag_Manager_tag_Id_tag_Type_del_Status",
                table: "sys_Tag_Manager",
                columns: new[] { "tag_Id", "tag_Type", "del_Status" });

            migrationBuilder.AddForeignKey(
                name: "FK_sys_Person_sys_Job_job_Id",
                table: "sys_Person",
                column: "job_Id",
                principalTable: "sys_Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sys_Person_sys_Job_job_Id",
                table: "sys_Person");

            migrationBuilder.DropTable(
                name: "sys_File");

            migrationBuilder.DropTable(
                name: "sys_Lingau");

            migrationBuilder.DropTable(
                name: "sys_Lingau_OptLog");

            migrationBuilder.DropTable(
                name: "sys_Order_TagOrReport");

            migrationBuilder.DropTable(
                name: "sys_Tag_Manager");

            migrationBuilder.DropIndex(
                name: "IX_sys_Person_job_Id",
                table: "sys_Person");

            migrationBuilder.DropColumn(
                name: "isEnable",
                table: "sys_User");

            migrationBuilder.DropColumn(
                name: "isEnable",
                table: "sys_Tag");

            migrationBuilder.DropColumn(
                name: "job_Id",
                table: "sys_Person");

            migrationBuilder.DropColumn(
                name: "isOuterJoin",
                table: "sys_Menu");

            migrationBuilder.DropColumn(
                name: "menu_ComponentName",
                table: "sys_Menu");

            migrationBuilder.DropColumn(
                name: "menu_FileName",
                table: "sys_Menu");

            migrationBuilder.DropColumn(
                name: "menu_Icon",
                table: "sys_Menu");

            migrationBuilder.DropColumn(
                name: "menu_Link",
                table: "sys_Menu");

            migrationBuilder.DropColumn(
                name: "log_ApiMethod",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_ApiPath",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_Browser",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_BrowserInfo",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_Device",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_ElapsedMilliseconds",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_OptPort",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_Os",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "log_Params",
                table: "sys_Log");

            migrationBuilder.DropColumn(
                name: "menu_IdArray",
                table: "sys_Limit_Menu");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "sys_Dict");

            migrationBuilder.DropColumn(
                name: "dict_Name",
                table: "sys_Dict");

            migrationBuilder.DropColumn(
                name: "isSystemBuilt",
                table: "sys_Dict");

            migrationBuilder.AlterColumn<Guid>(
                name: "person_Id",
                table: "sys_User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "limit_Id",
                table: "sys_User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_Id",
                table: "sys_User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "per_UserPwd",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "per_UserName",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "per_Type",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "per_NormalizedPhone",
                table: "sys_Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_Id",
                table: "sys_Person",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Person",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "order_StateId",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "order_EndPlaceId",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "order_EndLng",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "order_EndLat",
                table: "sys_Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "order_BenginPlaceId",
                table: "sys_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "menu_Path",
                table: "sys_Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Menu",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "menu_Type",
                table: "sys_Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "log_Type",
                table: "sys_Log",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "log_OptUser",
                table: "sys_Log",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "log_OptIP",
                table: "sys_Log",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "log_Message",
                table: "sys_Log",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Limit_Menu",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Limit",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "dept_Id",
                table: "sys_Job",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Job",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "dict_JsonValue",
                table: "sys_Dict",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "del_Status",
                table: "sys_Dept",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "dept_Level",
                table: "sys_Dept",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sys_Order_Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true),
                    order_Tag_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Order_Tag", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_sys_Order_Tag_sys_Order_order_Id",
                        column: x => x.order_Id,
                        principalTable: "sys_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sys_Order_Tag_sys_Tag_tag_Id",
                        column: x => x.tag_Id,
                        principalTable: "sys_Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_Tag_order_Id",
                table: "sys_Order_Tag",
                column: "order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_Tag_order_Tag_Name_order_Id_del_Status",
                table: "sys_Order_Tag",
                columns: new[] { "order_Tag_Name", "order_Id", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_Tag_tag_Id",
                table: "sys_Order_Tag",
                column: "tag_Id");
        }
    }
}
