using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestePraticoQualyTeam.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Processo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titulo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    codigo = table.Column<int>(type: "int", nullable: false),
                    categoria = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    processoID = table.Column<int>(type: "int", nullable: false),
                    nomeArquivo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    arquivo = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.id);
                    table.ForeignKey(
                        name: "FK_Documento_Processo_processoID",
                        column: x => x.processoID,
                        principalTable: "Processo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documento_codigo",
                table: "Documento",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documento_processoID",
                table: "Documento",
                column: "processoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Processo");
        }
    }
}
