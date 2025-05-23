using Microsoft.EntityFrameworkCore;
using SistemaVendasAplication.Models;

namespace SistemaVendasAplication.Data
{
    public class SysComAppDBContext : DbContext
    {
        public SysComAppDBContext(DbContextOptions<SysComAppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<ItemBudget> ItemBudget { get; set; }
        public DbSet<ItemSale> ItemSale { get; set; }
        public DbSet<ItemReturn> ItemReturn { get; set; }
    }
}
