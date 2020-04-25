using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarberShop.Clientes.Infra.Migrations
{
    public partial class AdicionandoServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                table: "Horario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horario_ServicoId",
                table: "Horario",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horario_Servico_ServicoId",
                table: "Horario",
                column: "ServicoId",
                principalTable: "Servico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horario_Servico_ServicoId",
                table: "Horario");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Horario_ServicoId",
                table: "Horario");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "Horario");
        }
    }
}
