using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exchange.Web.DataAccess.Migrations
{
    public partial class Add_new_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscussOfferEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<long>(nullable: false),
                    OwnerPhoneNumber = table.Column<string>(nullable: true),
                    PartnerPhoneNumber = table.Column<string>(nullable: true),
                    Conditions = table.Column<string>(nullable: true),
                    PartnerId = table.Column<long>(nullable: false),
                    OwnerPhotoOffer = table.Column<string>(nullable: true),
                    PartnerPhotoOffer = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussOfferEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussOfferEntities");
        }
    }
}
