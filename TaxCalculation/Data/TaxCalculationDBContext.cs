using Microsoft.EntityFrameworkCore;

namespace TaxCalculationAPI.Data
{
    public class TaxCalculationDBContext: DbContext
    {
        public TaxCalculationDBContext() : base()
        {
        }
        public TaxCalculationDBContext(DbContextOptions<TaxCalculationDBContext> options) : base(options)
        {

        }
        public DbSet<PostalCode> PostalCode { get; set; }
        public DbSet<CalculatedTax> CalculatedTax { get; set; }
        public DbSet<TaxType> TaxType { get; set; }
        public DbSet<CalculationType> CalculationType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCode>().ToTable("PostalCodes");
            modelBuilder.Entity<CalculatedTax>().ToTable("CalculatedTax");
            modelBuilder.Entity<TaxType>().ToTable("TaxType");
            modelBuilder.Entity<CalculationType>().ToTable("CalculationType");
        }
    }
}
