using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PA.ApplicationCore.Migrations
{
    /// <inheritdoc />
    public partial class addedMetricsToPostes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ref",
                table: "Postes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "CpuUsage",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemoryUsage",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpuUsage",
                table: "Postes");

            migrationBuilder.DropColumn(
                name: "MemoryUsage",
                table: "Postes");

            migrationBuilder.AlterColumn<string>(
                name: "Ref",
                table: "Postes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
