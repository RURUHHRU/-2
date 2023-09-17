using Microsoft.EntityFrameworkCore;

namespace 药店管理.mode_data
{
   
        public class MyDbContext : DbContext
        {
            public DbSet<LoginModel> Theuserlogson { get; set; }

            public DbSet<Category> Categories { get; set; }
        public DbSet<Category1> Categories1 { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=;User ID=sa;Password=;Database=药店管理;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

    
}
