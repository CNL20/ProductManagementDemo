using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class MyStoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public MyStoreContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đổi chuỗi kết nối này cho phù hợp với máy bạn 
                optionsBuilder.UseSqlServer("Server=.;Database=MyStoreDB;User Id=sa;Password=12345;");
            }
        }
    }
}
