using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceChat.Migrations
{
    public partial class retiradorequireddenomeutilizadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Utilizador",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Utilizador",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
