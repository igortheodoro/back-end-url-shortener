using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortener.Aplicacao.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlEncurtada = table.Column<string>(maxLength: 32, nullable: true),
                    UrlReal = table.Column<string>(maxLength: 1024, nullable: false),
                    Acessos = table.Column<int>(maxLength: 32, nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
