using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class albumaddreleasedate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "BandGenre",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From",
                table: "BandGenre",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "ReleaseDate",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Albums");

            migrationBuilder.AlterColumn<long>(
                name: "To",
                table: "BandGenre",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "From",
                table: "BandGenre",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
