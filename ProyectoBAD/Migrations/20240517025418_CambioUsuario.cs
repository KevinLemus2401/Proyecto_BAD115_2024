using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoBAD.Migrations
{
    public partial class CambioUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "USUARIO",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
