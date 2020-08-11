using Microsoft.EntityFrameworkCore.Migrations;

namespace Exchange.Web.DataAccess.Migrations
{
    public partial class Add_DBSet_Entity_ALL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessageStatusEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageStatusEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatPartyEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatPartyEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessageStatusEntities");

            migrationBuilder.DropTable(
                name: "ChatPartyEntities");
        }
    }
}
