using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculationAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateCalculationTypeRateDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RatePercentage",
                table: "CalculationType",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RatePercentage",
                table: "CalculationType",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
