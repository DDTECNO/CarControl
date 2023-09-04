using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizaNomeProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "flVaga",
                table: "Vaga",
                newName: "FlVaga");

            migrationBuilder.RenameColumn(
                name: "nmOperacao",
                table: "Operacao",
                newName: "NmOperacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FlVaga",
                table: "Vaga",
                newName: "flVaga");

            migrationBuilder.RenameColumn(
                name: "NmOperacao",
                table: "Operacao",
                newName: "nmOperacao");
        }
    }
}
