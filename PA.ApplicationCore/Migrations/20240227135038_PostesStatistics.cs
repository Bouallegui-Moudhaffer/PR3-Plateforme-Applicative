using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PA.ApplicationCore.Migrations
{
    /// <inheritdoc />
    public partial class PostesStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemoryUsage",
                table: "Postes",
                newName: "MemoryUsagePeak");

            migrationBuilder.RenameColumn(
                name: "CpuUsage",
                table: "Postes",
                newName: "MemoryUsageMedian");

            migrationBuilder.AddColumn<string>(
                name: "CpuUsageMean",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuUsageMedian",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuUsagePeak",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemoryUsageMean",
                table: "Postes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpuUsageMean",
                table: "Postes");

            migrationBuilder.DropColumn(
                name: "CpuUsageMedian",
                table: "Postes");

            migrationBuilder.DropColumn(
                name: "CpuUsagePeak",
                table: "Postes");

            migrationBuilder.DropColumn(
                name: "MemoryUsageMean",
                table: "Postes");

            migrationBuilder.RenameColumn(
                name: "MemoryUsagePeak",
                table: "Postes",
                newName: "MemoryUsage");

            migrationBuilder.RenameColumn(
                name: "MemoryUsageMedian",
                table: "Postes",
                newName: "CpuUsage");
        }
    }
}
