using Microsoft.EntityFrameworkCore;
using OrderManger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManger.DbContexts
{
    public class OrderManagerDbContext : DbContext
    {

        private static OrderManagerDbContext instance;
        public string Name { get; private set; }
        private static object syncRoot = new Object();

        private OrderManagerDbContext() {  }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Paper> Papers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ordermanager.db");
            
        }

        public static OrderManagerDbContext getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new OrderManagerDbContext();
                }
            }
            return instance;
        }
    }
}
