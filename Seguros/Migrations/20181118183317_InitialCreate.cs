using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seguros.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 100, nullable: false),
                    Percentage = table.Column<double>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(maxLength: 300, nullable: false),
                    Period = table.Column<int>(maxLength: 300, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Danger = table.Column<int>(maxLength: 300, nullable: false),
                    TypeId = table.Column<int>(maxLength: 300, nullable: false),
                    ClientId = table.Column<int>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policy_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Policy_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Email", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, "jane@prueba.com", "Austen", "Jane" },
                    { 2, "charles@prueba.com", "Dickens", "Charles" },
                    { 3, "miguel@prueba.com", "Cervantes", "Miguel" }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Percentage", "Value" },
                values: new object[,]
                {
                    { 1, 10.0, "Terremoto" },
                    { 2, 50.0, "Incendio" },
                    { 3, 80.0, "Pérdida" }
                });

            migrationBuilder.InsertData(
                table: "Policy",
                columns: new[] { "Id", "ClientId", "Danger", "Date", "Description", "Name", "Period", "Price", "TypeId" },
                values: new object[] { 1, 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Se está haciendo una prueba", "Prueba1", 12, 1000.0, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Policy_ClientId",
                table: "Policy",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_TypeId",
                table: "Policy",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
