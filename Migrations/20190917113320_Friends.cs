using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventApp.Migrations
{
    public partial class Friends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Places_PlaceIdentity",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Events_EventId",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_People_PersonId",
                table: "Invitations");

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    FriendPersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_People_FriendPersonId",
                        column: x => x.FriendPersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friend_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FriendPersonId",
                table: "Friend",
                column: "FriendPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_PersonId",
                table: "Friend",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Places_PlaceIdentity",
                table: "Events",
                column: "PlaceIdentity",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Events_EventId",
                table: "Invitations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_People_PersonId",
                table: "Invitations",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Places_PlaceIdentity",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Events_EventId",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_People_PersonId",
                table: "Invitations");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Places_PlaceIdentity",
                table: "Events",
                column: "PlaceIdentity",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Events_EventId",
                table: "Invitations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_People_PersonId",
                table: "Invitations",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
