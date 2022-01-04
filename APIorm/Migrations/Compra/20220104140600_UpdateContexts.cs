using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIorm.Migrations.Compra
{
    public partial class UpdateContexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Usuario_UsuarioSolicitante",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCompras_Compras_IdCompra",
                table: "ItensCompras");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCompras_Produtos_IdProduto",
                table: "ItensCompras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItensCompras",
                table: "ItensCompras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compras",
                table: "Compras");

            migrationBuilder.RenameTable(
                name: "Produtos",
                newName: "Produto");

            migrationBuilder.RenameTable(
                name: "ItensCompras",
                newName: "ItensCompra");

            migrationBuilder.RenameTable(
                name: "Compras",
                newName: "Compra");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_Codigo",
                table: "Produto",
                newName: "IX_Produto_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_ItensCompras_IdProduto",
                table: "ItensCompra",
                newName: "IX_ItensCompra_IdProduto");

            migrationBuilder.RenameIndex(
                name: "IX_ItensCompras_IdCompra",
                table: "ItensCompra",
                newName: "IX_ItensCompra_IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_UsuarioSolicitante",
                table: "Compra",
                newName: "IX_Compra_UsuarioSolicitante");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produto",
                table: "Produto",
                column: "IdProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensCompra",
                table: "ItensCompra",
                column: "IdItemCompra");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compra",
                table: "Compra",
                column: "IdCompra");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Usuario_UsuarioSolicitante",
                table: "Compra",
                column: "UsuarioSolicitante",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCompra_Compra_IdCompra",
                table: "ItensCompra",
                column: "IdCompra",
                principalTable: "Compra",
                principalColumn: "IdCompra",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCompra_Produto_IdProduto",
                table: "ItensCompra",
                column: "IdProduto",
                principalTable: "Produto",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Usuario_UsuarioSolicitante",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCompra_Compra_IdCompra",
                table: "ItensCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCompra_Produto_IdProduto",
                table: "ItensCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produto",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItensCompra",
                table: "ItensCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compra",
                table: "Compra");

            migrationBuilder.RenameTable(
                name: "Produto",
                newName: "Produtos");

            migrationBuilder.RenameTable(
                name: "ItensCompra",
                newName: "ItensCompras");

            migrationBuilder.RenameTable(
                name: "Compra",
                newName: "Compras");

            migrationBuilder.RenameIndex(
                name: "IX_Produto_Codigo",
                table: "Produtos",
                newName: "IX_Produtos_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_ItensCompra_IdProduto",
                table: "ItensCompras",
                newName: "IX_ItensCompras_IdProduto");

            migrationBuilder.RenameIndex(
                name: "IX_ItensCompra_IdCompra",
                table: "ItensCompras",
                newName: "IX_ItensCompras_IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_UsuarioSolicitante",
                table: "Compras",
                newName: "IX_Compras_UsuarioSolicitante");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos",
                column: "IdProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensCompras",
                table: "ItensCompras",
                column: "IdItemCompra");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compras",
                table: "Compras",
                column: "IdCompra");

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "IdProduto",
                keyValue: 1L,
                column: "DataHoraCadastro",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "IdProduto",
                keyValue: 2L,
                column: "DataHoraCadastro",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Usuario_UsuarioSolicitante",
                table: "Compras",
                column: "UsuarioSolicitante",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCompras_Compras_IdCompra",
                table: "ItensCompras",
                column: "IdCompra",
                principalTable: "Compras",
                principalColumn: "IdCompra",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCompras_Produtos_IdProduto",
                table: "ItensCompras",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "IdProduto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
