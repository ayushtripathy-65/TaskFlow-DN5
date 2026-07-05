using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 5, 13, 42, 29, 456, DateTimeKind.Utc).AddTicks(7098), "admin@taskflow.com", "Admin", "admin123", "Admin" },
                    { 2, new DateTime(2026, 7, 5, 13, 42, 29, 456, DateTimeKind.Utc).AddTicks(7110), "manager@taskflow.com", "Manager", "manager123", "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 5, 13, 42, 29, 456, DateTimeKind.Utc).AddTicks(7310), "Redesign company website", "Website Redesign", 2 },
                    { 2, new DateTime(2026, 7, 5, 13, 42, 29, 456, DateTimeKind.Utc).AddTicks(7319), "Build iOS/Android app", "Mobile App", 2 }
                });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Priority", "ProjectId", "Status", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 5, 13, 42, 29, 456, DateTimeKind.Utc).AddTicks(7368), "", new DateTime(2026, 7, 12, 19, 12, 29, 456, DateTimeKind.Local).AddTicks(7380), "High", 1, "InProgress", "Design homepage" },
                    { 2, new DateTime(2026, 7, 5, 13, 42, 29, 456, DateTimeKind.Utc).AddTicks(7405), "", new DateTime(2026, 7, 3, 19, 12, 29, 456, DateTimeKind.Local).AddTicks(7408), "High", 1, "Done", "Setup API" },
                    { 3, new DateTime(2026, 7, 5, 13, 42, 29, 456, DateTimeKind.Utc).AddTicks(7409), "", new DateTime(2026, 7, 8, 19, 12, 29, 456, DateTimeKind.Local).AddTicks(7411), "Medium", 2, "Pending", "Write tests" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ProjectId",
                table: "TaskItems",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
