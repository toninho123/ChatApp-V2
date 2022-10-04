using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceChat.Migrations
{
    public partial class ModificacaoModelMensagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Mensagem");

            migrationBuilder.DropColumn(
                name: "Data_Mensagem",
                table: "Mensagem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Mensagem",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Mensagem",
                table: "Mensagem",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
