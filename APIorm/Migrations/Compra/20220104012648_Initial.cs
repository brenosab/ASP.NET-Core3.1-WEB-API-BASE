using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIorm.Migrations.Compra
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProduto = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    DataHoraCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NomeLogin = table.Column<string>(nullable: true),
                    SenhaLogin = table.Column<byte[]>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Sexo = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: false),
                    TipoUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    IdCompra = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLoja = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ObservacaoCompra = table.Column<string>(nullable: true),
                    UsuarioSolicitante = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.IdCompra);
                    table.ForeignKey(
                        name: "FK_Compras_Usuario_UsuarioSolicitante",
                        column: x => x.UsuarioSolicitante,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensCompras",
                columns: table => new
                {
                    IdItemCompra = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompra = table.Column<long>(nullable: false),
                    IdProduto = table.Column<long>(nullable: false),
                    ValorUnit = table.Column<double>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCompras", x => x.IdItemCompra);
                    table.ForeignKey(
                        name: "FK_ItensCompras_Compras_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compras",
                        principalColumn: "IdCompra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCompras_Produtos_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produtos",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "IdProduto", "Codigo", "DataHoraCadastro", "Descricao", "Valor" },
                values: new object[] { 1L, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ALCOOL EM GEL 1L", 8.1999999999999993 });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "IdProduto", "Codigo", "DataHoraCadastro", "Descricao", "Valor" },
                values: new object[] { 2L, 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MÁSCARA", 1.3999999999999999 });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioSolicitante",
                table: "Compras",
                column: "UsuarioSolicitante");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompras_IdCompra",
                table: "ItensCompras",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompras_IdProduto",
                table: "ItensCompras",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Codigo",
                table: "Produtos",
                column: "Codigo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensCompras");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
