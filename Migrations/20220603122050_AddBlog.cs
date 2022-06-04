using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHWorld.Migrations
{
    public partial class AddBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vnumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vnumb = table.Column<string>(type: "nvarchar(max)", nullable: false),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vnumber", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vnumber");
        }
    }
}
