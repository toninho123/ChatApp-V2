using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceChat.Migrations
{
    public partial class AdministradorBooleano : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Administrador",
                table: "Grupo",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Administrador",
                table: "Grupo",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }
    }
}
