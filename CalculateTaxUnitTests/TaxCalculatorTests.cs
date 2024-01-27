using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;
using TaxCalculationAPI.Services;

namespace CalculateTaxUnitTests
{
    [TestFixture]
    internal class TaxCalculatorTests
    {
        private TaxCalculationDBContext dbContext;
        private ITaxCalculatorFactory taxCalculatorFactory;

        public TaxCalculatorTests(TaxCalculationDBContext context, ITaxCalculatorFactory factory)
        {
            dbContext = context;
            taxCalculatorFactory = factory; 
        }
        [Test]
        public void CalculateTax_ProgressiveTax_ReturnsCorrectTax()
        {
            var taxCalculatorFactoryMock = new Mock<ITaxCalculatorFactory>();
            var dbContextMock = dbContext;
            var calculatorFactory = new TaxCalculatorFactory();
            var taxService = new TaxService(taxCalculatorFactory, dbContextMock);

            var tax = taxService.CalculateTax("7441", 50000);

            Assert.AreEqual(7500, tax);
        }

        [Test]
        public void CalculateTax_FlatValueTax_ReturnsCorrectTax()
        {
            var taxCalculatorFactoryMock = new Mock<ITaxCalculatorFactory>();
            var dbContextMock = new Mock<TaxCalculationDBContext>();
            var calculatorFactory = new TaxCalculatorFactory();
            var taxService = new TaxService(taxCalculatorFactoryMock.Object, dbContextMock.Object);

            var tax = taxService.CalculateTax("A100", 50000);

            Assert.AreEqual(10000, tax);
        }

        [Test]
        public void CalculateTax_FlatRateTax_ReturnsCorrectTax()
        {
            var taxCalculatorFactoryMock = new Mock<ITaxCalculatorFactory>();
            var dbContextMock = new Mock<TaxCalculationDBContext>();
            var calculatorFactory = new TaxCalculatorFactory();
            var taxService = new TaxService(taxCalculatorFactoryMock.Object, dbContextMock.Object);

            var tax = taxService.CalculateTax("7000", 50000);

            Assert.AreEqual(8750, tax);
        }

        [Test]
        public void CalculateTax_WithValidPostalCode_ReturnsCorrectTax()
        {
            // Arrange
            var taxCalculatorFactoryMock = new Mock<ITaxCalculatorFactory>();
            var dbContextMock = new Mock<TaxCalculationDBContext>(); // Replace YourDbContext with the actual name of your DbContext.

            var taxService = new TaxService(taxCalculatorFactoryMock.Object, dbContextMock.Object);

            // Mock data for testing
            var postalCode = "7441";
            var annualIncome = 50000m;

            var taxRateFromDatabase = new TaxRate { PostalCode = postalCode, RatePercentage = 0.15m };
            //dbContextMock.Setup(db => db.CalculationType)
           //              .Returns(new List<TaxRate> { taxRateFromDatabase }.AsQueryable());

            // Act
            var tax = taxService.CalculateTax(postalCode, annualIncome);

            // Assert
            Assert.AreEqual(7525m, tax); // Adjust this based on the expected result for your specific test case
        }

        [Test]
        public void CalculateTax_WithInvalidPostalCode_ReturnsDefaultTax()
        {
            // Arrange
            var taxCalculatorFactoryMock = new Mock<ITaxCalculatorFactory>();
            var dbContextMock = new Mock<TaxCalculationDBContext>(); // Replace YourDbContext with the actual name of your DbContext.

            var taxService = new TaxService(taxCalculatorFactoryMock.Object, dbContextMock.Object);

            // Mock data for testing
            var postalCode = "InvalidCode";
            var annualIncome = 50000m;

           // dbContextMock.Setup(db => db.CalculationType)
           //              .Returns(new List<TaxRate>().AsQueryable());

            // Act
            var tax = taxService.CalculateTax(postalCode, annualIncome);

            // Assert
            Assert.AreEqual(5000m, tax); // Adjust this based on the expected result for your specific test case
        }

        // Add more test cases based on your requirements
    }

    // Mock class for testing purposes
    public class TaxRate
    {
        public string PostalCode { get; set; }
        public decimal RatePercentage { get; set; }
    }
}

