using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RondaSegurancaBack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarOcorrenciaComImagemGps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "Ocorrencias");

            migrationBuilder.AddColumn<string>(
                name: "ImagemPath",
                table: "Ocorrencias",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Ocorrencias",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Ocorrencias",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Ocorrencias",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemPath",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Ocorrencias");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "Ocorrencias",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
