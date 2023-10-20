using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.EntityFramework.Core.Migrations.SystemDb
{
    /// <inheritdoc />
    public partial class initSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_Dept",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dept_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dept_FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dept_SimpleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dept_ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    dept_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dept_PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dept_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Dept", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Dict",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    dict_Key = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    dict_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dict_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dict_JsonValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isSystemBuilt = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_sys_Dict", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

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
                name: "sys_Limit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    limit_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Limit", x => x.Id)
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
                name: "sys_Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    log_OptIP = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    log_OptUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    log_Type = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    log_Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_OptTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    log_Params = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_ApiMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_ApiPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_ElapsedMilliseconds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_Os = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_Device = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_BrowserInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_OptPort = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_sys_Log", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    menu_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    menu_FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menu_SimpleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isOuterJoin = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    menu_Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menu_Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menu_FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menu_ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menu_Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menu_PlatFrom = table.Column<int>(type: "int", nullable: true),
                    menu_ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Menu", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_RiderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_RiderPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_JobStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_ReceiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_GoodsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_GoodsType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_GoodsWeight = table.Column<double>(type: "float", nullable: false),
                    order_GoddsNums = table.Column<int>(type: "int", nullable: false),
                    order_GoodsLong = table.Column<double>(type: "float", nullable: false),
                    order_GoodsWidth = table.Column<double>(type: "float", nullable: false),
                    order_GoodsHight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_GoodsPrice = table.Column<double>(type: "float", nullable: false),
                    order_BenginLng = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    order_BenginLat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    order_BenginPlaceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_EndLng = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    order_EndLat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    order_EndPlaceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_StateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_PathDistance = table.Column<double>(type: "float", nullable: false),
                    order_GoodsDelivery = table.Column<double>(type: "float", nullable: false),
                    order_PayType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_PayIdentity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_CreateDeptId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order_CreateDeptName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_sys_Order", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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
                    table.PrimaryKey("PK_sys_Tag", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "sys_Job",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    job_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dept_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Job", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_sys_Job_sys_Dept_dept_Id",
                        column: x => x.dept_Id,
                        principalTable: "sys_Dept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sys_Limit_Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    limit_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    menu_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    menu_IdArray = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    limit_Add = table.Column<bool>(type: "bit", nullable: true),
                    limit_Del = table.Column<bool>(type: "bit", nullable: true),
                    limit_Edit = table.Column<bool>(type: "bit", nullable: true),
                    limit_Query = table.Column<bool>(type: "bit", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Limit_Menu", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_sys_Limit_Menu_sys_Limit_limit_Id",
                        column: x => x.limit_Id,
                        principalTable: "sys_Limit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sys_Limit_Menu_sys_Menu_menu_Id",
                        column: x => x.menu_Id,
                        principalTable: "sys_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "sys_Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    per_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    per_FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_SimpleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_JobStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_IdNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_Birthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_Politics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dept_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    job_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    per_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_UserPwd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    per_NormalizedPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_Person", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_sys_Person_sys_Dept_dept_Id",
                        column: x => x.dept_Id,
                        principalTable: "sys_Dept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sys_Person_sys_Job_job_Id",
                        column: x => x.job_Id,
                        principalTable: "sys_Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sys_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_LoginName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_LoginPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_LoginPwdCipher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isEnable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    dept_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    person_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    limit_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    create_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    update_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    del_Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    del_Status = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    expand_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expand_Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_User", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_sys_User_sys_Dept_dept_Id",
                        column: x => x.dept_Id,
                        principalTable: "sys_Dept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sys_User_sys_Limit_limit_Id",
                        column: x => x.limit_Id,
                        principalTable: "sys_Limit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sys_User_sys_Person_person_Id",
                        column: x => x.person_Id,
                        principalTable: "sys_Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Dept_del_Status",
                table: "sys_Dept",
                column: "del_Status");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Dict_dict_Key_del_Status",
                table: "sys_Dict",
                columns: new[] { "dict_Key", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_File_del_Status",
                table: "sys_File",
                column: "del_Status");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Job_del_Status_dept_Id",
                table: "sys_Job",
                columns: new[] { "del_Status", "dept_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Job_dept_Id",
                table: "sys_Job",
                column: "dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Limit_del_Status",
                table: "sys_Limit",
                column: "del_Status");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Limit_Menu_del_Status_limit_Id_menu_Id",
                table: "sys_Limit_Menu",
                columns: new[] { "del_Status", "limit_Id", "menu_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Limit_Menu_limit_Id",
                table: "sys_Limit_Menu",
                column: "limit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Limit_Menu_menu_Id",
                table: "sys_Limit_Menu",
                column: "menu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Lingau_person_Id_del_Status",
                table: "sys_Lingau",
                columns: new[] { "person_Id", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Lingau_OptLog_opt_User_opt_BeUser_del_Status",
                table: "sys_Lingau_OptLog",
                columns: new[] { "opt_User", "opt_BeUser", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Log_log_OptIP_log_OptUser_log_Type_del_Status",
                table: "sys_Log",
                columns: new[] { "log_OptIP", "log_OptUser", "log_Type", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Menu_del_Status",
                table: "sys_Menu",
                column: "del_Status");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_del_Status",
                table: "sys_Order",
                column: "del_Status");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_TagOrReport_order_Id",
                table: "sys_Order_TagOrReport",
                column: "order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Order_TagOrReport_order_TagOrReport_Name_order_Id_del_Status",
                table: "sys_Order_TagOrReport",
                columns: new[] { "order_TagOrReport_Name", "order_Id", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Person_del_Status_dept_Id",
                table: "sys_Person",
                columns: new[] { "del_Status", "dept_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Person_dept_Id",
                table: "sys_Person",
                column: "dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Person_job_Id",
                table: "sys_Person",
                column: "job_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_Tag_tag_Name_del_Status",
                table: "sys_Tag",
                columns: new[] { "tag_Name", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_Tag_Manager_tag_Id_tag_Type_del_Status",
                table: "sys_Tag_Manager",
                columns: new[] { "tag_Id", "tag_Type", "del_Status" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_User_del_Status_limit_Id_person_Id",
                table: "sys_User",
                columns: new[] { "del_Status", "limit_Id", "person_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_sys_User_dept_Id",
                table: "sys_User",
                column: "dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_User_limit_Id",
                table: "sys_User",
                column: "limit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_sys_User_person_Id",
                table: "sys_User",
                column: "person_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_Dict");

            migrationBuilder.DropTable(
                name: "sys_File");

            migrationBuilder.DropTable(
                name: "sys_Limit_Menu");

            migrationBuilder.DropTable(
                name: "sys_Lingau");

            migrationBuilder.DropTable(
                name: "sys_Lingau_OptLog");

            migrationBuilder.DropTable(
                name: "sys_Log");

            migrationBuilder.DropTable(
                name: "sys_Order_TagOrReport");

            migrationBuilder.DropTable(
                name: "sys_Tag_Manager");

            migrationBuilder.DropTable(
                name: "sys_User");

            migrationBuilder.DropTable(
                name: "sys_Menu");

            migrationBuilder.DropTable(
                name: "sys_Order");

            migrationBuilder.DropTable(
                name: "sys_Tag");

            migrationBuilder.DropTable(
                name: "sys_Limit");

            migrationBuilder.DropTable(
                name: "sys_Person");

            migrationBuilder.DropTable(
                name: "sys_Job");

            migrationBuilder.DropTable(
                name: "sys_Dept");
        }
    }
}
