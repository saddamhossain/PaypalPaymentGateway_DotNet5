using Microsoft.EntityFrameworkCore;
using PaypalPaymentGateway_DotNet5.Models;

namespace PaypalPaymentGateway_DotNet5.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
