using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crudProdutos.Migrations
{
    public partial class Version02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Telefone",
                table: "Usuarios",
                maxLength: 11,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Bairro",
                table: "Usuarios",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_CEP",
                table: "Usuarios",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cidade",
                table: "Usuarios",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Complemento",
                table: "Usuarios",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Estado",
                table: "Usuarios",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Logradouro",
                table: "Usuarios",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Numero",
                table: "Usuarios",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Referencia",
                table: "Usuarios",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    DataHora = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => new { x.IdUsuario, x.IdProduto });
                    table.ForeignKey(
                        name: "FK_Favoritos_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoritos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataHoraPedido = table.Column<DateTime>(nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: true),
                    Endereco_Logradouro = table.Column<string>(maxLength: 100, nullable: true),
                    Endereco_Numero = table.Column<string>(maxLength: 10, nullable: true),
                    Endereco_Complemento = table.Column<string>(maxLength: 100, nullable: true),
                    Endereco_Bairro = table.Column<string>(maxLength: 50, nullable: true),
                    Endereco_Cidade = table.Column<string>(maxLength: 50, nullable: true),
                    Endereco_Estado = table.Column<string>(maxLength: 2, nullable: true),
                    Endereco_CEP = table.Column<string>(maxLength: 2, nullable: true),
                    Endereco_Referencia = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visitados",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    DataHora = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitados", x => new { x.IdUsuario, x.IdProduto });
                    table.ForeignKey(
                        name: "FK_Visitados_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitados_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedido",
                columns: table => new
                {
                    IdPedido = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ValorVoluntario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedido", x => new { x.IdPedido, x.IdProduto });
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_IdProduto",
                table: "Favoritos",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_IdProduto",
                table: "ItensPedido",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Visitados_IdProduto",
                table: "Visitados",
                column: "IdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "ItensPedido");

            migrationBuilder.DropTable(
                name: "Visitados");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_Bairro",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_CEP",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_Cidade",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_Complemento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_Estado",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_Logradouro",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_Numero",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco_Referencia",
                table: "Usuarios");
        }
    }
}
