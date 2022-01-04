using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIorm.Migrations.Compra
{
    public partial class UpdateContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "IdProduto",
                keyValue: 1L,
                column: "DataHoraCadastro",
                value: new DateTime(2022, 1, 4, 11, 8, 14, 846, DateTimeKind.Local).AddTicks(9875));

            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "IdProduto",
                keyValue: 2L,
                column: "DataHoraCadastro",
                value: new DateTime(2022, 1, 4, 11, 8, 14, 848, DateTimeKind.Local).AddTicks(2203));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "IdProduto",
                keyValue: 1L,
                column: "DataHoraCadastro",
                value: new DateTime(2022, 1, 4, 11, 6, 0, 203, DateTimeKind.Local).AddTicks(3378));

            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "IdProduto",
                keyValue: 2L,
                column: "DataHoraCadastro",
                value: new DateTime(2022, 1, 4, 11, 6, 0, 204, DateTimeKind.Local).AddTicks(4536));
        }
    }
}
