using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceChat.Migrations
{
    public partial class RemovedTextoLimitMensagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Mensagem",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Mensagem",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
