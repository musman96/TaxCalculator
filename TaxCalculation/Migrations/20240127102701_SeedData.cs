using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculationAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaxType",
                columns: new[] { "ID", "Type", "Description", "IsActive" },
                values: new object[,]
                {
                    { 1, "Progressive", "Progressive Tax Type", true },
                    { 2, "Flat Value", "Flat Value Tax Type", true },
                    { 3, "Flat Rate", "Flat Rate Tax Type", true },
                });

            migrationBuilder.InsertData(
                table: "PostalCodes",
                columns: new[] { "ID", "Code", "TaxCalculationTypeID", "IsActive" },
                values: new object[,]
                {
                    { 1, "7441", 1, true },
                    { 2, "A100", 2, true },
                    { 3, "7000", 3, true },
                    { 4, "1000", 1, true },
                });

            migrationBuilder.InsertData(
                table: "CalculationType",
                columns: new[] { "ID", "TaxCalculationTypeID", "RatePercentage", "RateFrom", "RateTo", "IsActive" },
                values: new object[,]
                {
                    { 1, 1, 10.00m, 0.00m, 8350.00m, true },
                    { 2, 1, 15.00m, 8351.00m, 33950.00m, true },
                    { 3, 1, 25.00m, 33951.00m, 82250.00m, true },
                    { 4, 1, 28.00m, 82251.00m, 171550.00m, true },
                    { 5, 1, 33.00m, 171551.00m, 372950.00m, true },
                    { 6, 1, 35.00m, 372951.00m, 372951.00m, true },
                    { 7, 2, 0.00m, 10000.00m, 10000.00m, true },
                    { 8, 2, 5.00m, 0.00m, 200000.00m, true },
                    { 9, 3, 17.50m, 0.00m, 0.00m, true },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
