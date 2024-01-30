using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaGestion_API.Migrations
{
    /// <inheritdoc />
    public partial class ArreglandoEntidadesR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_ProyectoId",
                table: "Asignaciones",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_UsuarioId",
                table: "Asignaciones",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Proyectos_ProyectoId",
                table: "Asignaciones",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Usuarios_UsuarioId",
                table: "Asignaciones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Proyectos_ProyectoId",
                table: "Asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Usuarios_UsuarioId",
                table: "Asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_ProyectoId",
                table: "Asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_UsuarioId",
                table: "Asignaciones");

            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "Id", "Descripcion", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "Nombre" },
                values: new object[,]
                {
                    { 1, "Este es el proyecto de realizacion de censo", new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(436), new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(412), "", "Proyecto censo" },
                    { 2, "Este es el proyecto de embellecimiento de parques", new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(440), new DateTime(2024, 1, 27, 19, 7, 50, 959, DateTimeKind.Local).AddTicks(439), "", "Proyecto embellecimiento" }
                });
        }
    }
}
