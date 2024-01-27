using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculationAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressiveTax");

            migrationBuilder.DropTable(
                name: "TaxCalculationTypes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CalculatedTax",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CalculationType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxCalculationTypeID = table.Column<int>(type: "int", nullable: false),
                    RatePercentage = table.Column<int>(type: "int", nullable: false),
                    RateFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxType", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculationType");

            migrationBuilder.DropTable(
                name: "TaxType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CalculatedTax");

            migrationBuilder.CreateTable(
                name: "ProgressiveTax",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RateFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RatePercentage = table.Column<int>(type: "int", nullable: false),
                    RateTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveTax", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationTypes", x => x.ID);
                });
        }
    }
}
