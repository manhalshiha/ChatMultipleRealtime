using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatMultipleRealtime.Server.data.migrations
{
    public partial class changeContetnToContentInMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contetn",
                table: "Message",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Message",
                newName: "Contetn");
        }
    }
}
