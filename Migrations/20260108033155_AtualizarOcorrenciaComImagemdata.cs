using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RondaSegurancaBack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarOcorrenciaComImagemdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencias_Rondas_RondaId",
                table: "Ocorrencias");

            migrationBuilder.AlterColumn<int>(
                name: "RondaId",
                table: "Ocorrencias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "Ocorrencias",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Rondas_RondaId",
                table: "Ocorrencias",
                column: "RondaId",
                principalTable: "Rondas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencias_Rondas_RondaId",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "Ocorrencias");

            migrationBuilder.AlterColumn<int>(
                name: "RondaId",
                table: "Ocorrencias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Rondas_RondaId",
                table: "Ocorrencias",
                column: "RondaId",
                principalTable: "Rondas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
