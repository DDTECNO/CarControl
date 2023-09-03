using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class corrigindoMovimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movimento",
                table: "Movimento");

            migrationBuilder.DropColumn(
                name: "IdVeiculo",
                table: "Movimento");

            migrationBuilder.AlterColumn<int>(
                name: "IdMovimento",
                table: "Movimento",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movimento",
                table: "Movimento",
                column: "IdMovimento");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Veiculo_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                principalTable: "Veiculo",
                principalColumn: "IdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Veiculo_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movimento",
                table: "Movimento");

            migrationBuilder.AlterColumn<int>(
                name: "IdMovimento",
                table: "Movimento",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "IdVeiculo",
                table: "Movimento",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movimento",
                table: "Movimento",
                columns: new[] { "IdMovimento", "IdVeiculo" });
        }
    }
}
