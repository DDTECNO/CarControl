using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizaRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Vaga_IdVaga",
                table: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento");

            migrationBuilder.AlterColumn<string>(
                name: "nmOperacao",
                table: "Operacao",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Vaga_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                principalTable: "Vaga",
                principalColumn: "IdVaga",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Vaga_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.AlterColumn<string>(
                name: "nmOperacao",
                table: "Operacao",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento",
                column: "IdVaga",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Vaga_IdVaga",
                table: "Movimento",
                column: "IdVaga",
                principalTable: "Vaga",
                principalColumn: "IdVaga",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
