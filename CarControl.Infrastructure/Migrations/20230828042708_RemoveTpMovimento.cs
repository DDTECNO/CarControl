﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTpMovimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TpMovimento",
                table: "Movimento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<char>(
                name: "TpMovimento",
                table: "Movimento",
                type: "TEXT",
                nullable: false,
                defaultValue: ' ');
        }
    }
}
