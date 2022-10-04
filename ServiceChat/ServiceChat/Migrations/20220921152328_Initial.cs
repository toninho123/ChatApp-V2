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
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Numero_Identificacao = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Id_Curso = table.Column<int>(type: "int", nullable: false),
                    Id_Entidade = table.Column<int>(type: "int", nullable: false),
                    Capa = table.Column<string>(type: "text", nullable: true),
                    Dt_Criado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sala_Curso_Id_Curso",
                        column: x => x.Id_Curso,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sala_Empresa_Id_Entidade",
                        column: x => x.Id_Entidade,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Administrador = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Dt_Criado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Saiu = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Lido = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false),
                    Id_Grupo = table.Column<int>(type: "int", nullable: false),
                    Id_Utilizador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupo_Sala_Id_Grupo",
                        column: x => x.Id_Grupo,
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
                    Id_Utilizador = table.Column<int>(type: "int", nullable: false),
                    Id_Grupo = table.Column<int>(type: "int", nullable: false),
                    Dt_Criado = table.Column<DateTime>(type: "datetime", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: true),
                    Anexo = table.Column<string>(type: "text", nullable: true),
                    Anexo_Nome = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Data_Mensagem = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagem_Sala_Id_Grupo",
                        column: x => x.Id_Grupo,
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
                name: "IX_Grupo_Id_Grupo",
                table: "Grupo",
                column: "Id_Grupo");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Id_Utilizador",
                table: "Grupo",
                column: "Id_Utilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_Id_Grupo",
                table: "Mensagem",
                column: "Id_Grupo");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_Id_Utilizador",
                table: "Mensagem",
                column: "Id_Utilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_Id_Curso",
                table: "Sala",
                column: "Id_Curso");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_Id_Entidade",
                table: "Sala",
                column: "Id_Entidade");
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

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
