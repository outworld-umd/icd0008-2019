using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations {

    public partial class InitialMigration : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new {
                    Name = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Games", x => x.Name); });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new {
                    SettingsId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardHeight = table.Column<int>(nullable: false),
                    BoardWidth = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Settings", x => x.SettingsId); });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }

}