using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace resource.Migrations
{
    public partial class PreviousMigrationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstProfileId = table.Column<int>(type: "int", nullable: false),
                    SecondProfileId = table.Column<int>(type: "int", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Profiles_FirstProfileId",
                        column: x => x.FirstProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_Profiles_SecondProfileId",
                        column: x => x.SecondProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FirstProfileId",
                table: "Contacts",
                column: "FirstProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SecondProfileId",
                table: "Contacts",
                column: "SecondProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
