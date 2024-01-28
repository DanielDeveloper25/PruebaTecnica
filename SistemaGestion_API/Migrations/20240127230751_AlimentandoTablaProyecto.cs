using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaGestion_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentandoTablaProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "Id", "Descripcion", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "Nombre" },
                values: new object[,]
                {
                    { 1, "Este es el proyecto de realizacion de censo", new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(436), new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(412), "", "Proyecto censo" },
                    { 2, "Este es el proyecto de embellecimiento de parques", new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(440), new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(439), "", "Proyecto embellecimiento" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
