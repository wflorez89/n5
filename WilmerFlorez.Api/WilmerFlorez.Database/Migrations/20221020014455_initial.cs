using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WilmerFlorez.Database.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Wf");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Wf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                schema: "Wf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "Wf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalSchema: "Wf",
                        principalTable: "PermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePermisions",
                schema: "Wf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePermisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePermisions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Wf",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePermisions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Wf",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermisions_EmployeeId",
                schema: "Wf",
                table: "EmployeePermisions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermisions_PermissionId",
                schema: "Wf",
                table: "EmployeePermisions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeId",
                schema: "Wf",
                table: "Permissions",
                column: "PermissionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePermisions",
                schema: "Wf");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Wf");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "Wf");

            migrationBuilder.DropTable(
                name: "PermissionTypes",
                schema: "Wf");
        }
    }
}
