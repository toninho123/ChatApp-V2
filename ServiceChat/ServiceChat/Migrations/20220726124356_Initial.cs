using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ServiceChat.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    isAtiva = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Numero_Aluno = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Funcao = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    Administrador = table.Column<int>(type: "int", nullable: false),
                    Id_Sala = table.Column<int>(type: "int", nullable: false),
                    Id_Utilizador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupo_Sala_Id_Sala",
                        column: x => x.Id_Sala,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupo_Utilizador_Id_Utilizador",
                        column: x => x.Id_Utilizador,
                        principalTable: "Utilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Texto = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Ficheiro = table.Column<string>(type: "text", nullable: true),
                    Data_Mensagem = table.Column<DateTime>(type: "datetime", nullable: false),
                    Id_Sala = table.Column<int>(type: "int", nullable: false),
                    Id_Utilizador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagem_Sala_Id_Sala",
                        column: x => x.Id_Sala,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensagem_Utilizador_Id_Utilizador",
                        column: x => x.Id_Utilizador,
                        principalTable: "Utilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Id_Sala",
                table: "Grupo",
                column: "Id_Sala");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Id_Utilizador",
                table: "Grupo",
                column: "Id_Utilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_Id_Sala",
                table: "Mensagem",
                column: "Id_Sala");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_Id_Utilizador",
                table: "Mensagem",
                column: "Id_Utilizador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Mensagem");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Utilizador");
        }
    }
}
