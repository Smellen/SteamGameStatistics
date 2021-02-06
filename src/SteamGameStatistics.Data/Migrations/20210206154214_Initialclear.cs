using Microsoft.EntityFrameworkCore.Migrations;

namespace SteamGameStatistics.Data.Migrations
{
    public partial class Initialclear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Steamid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommunityVisibilityState = table.Column<long>(type: "bigint", nullable: false),
                    Profilestate = table.Column<long>(type: "bigint", nullable: false),
                    PersonaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarMedium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarFull = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastlogoff = table.Column<long>(type: "bigint", nullable: false),
                    PersonaState = table.Column<long>(type: "bigint", nullable: false),
                    RealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryClanid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreated = table.Column<long>(type: "bigint", nullable: false),
                    PersonaStateFlags = table.Column<long>(type: "bigint", nullable: false),
                    LocCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocStateCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Steamid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
