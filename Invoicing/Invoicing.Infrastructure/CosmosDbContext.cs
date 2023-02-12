using Invoicing.Domain;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.Infrastructure
{
    public class CosmosDbContext : DbContext
    {
        public DbSet<Invoice>? Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "https://cosmos-db-assessment.documents.azure.com:443/",
                "K2JatS1K06KfWkC07FdP8FB24XoWDdqV62typLAcTX9BcDuEEF8JM3aanejrKSTxH2TtfWWlOQUsACDbr37ueg==",
                "invoicing-db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuring Invoices
            modelBuilder.Entity<Invoice>()
                .ToContainer("Invoices") // ToContainer
                .HasPartitionKey(e => e.Id); // Partition Key  

            modelBuilder.Entity<Invoice>().OwnsMany(p => p.InvoiceLines);
        }
    }

}
