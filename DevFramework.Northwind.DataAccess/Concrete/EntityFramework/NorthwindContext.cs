using DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Data.Entity;
using System.Reflection;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        // ConnectionString'i Database'e sorgu yollayacak Assembly'nin App.config dosyasında context'imizle aynı isimde olacak şekilde belirtmemiz yeterli olacaktır. 
        public NorthwindContext()
        {
            // Null değeri ile halihazırda verilerle dolu database'e sahip olduğumuz için bunları korumak adına Initializer'ı kapatıyoruz. 
            Database.SetInitializer<NorthwindContext>(null);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Mapping işlemlerimizi burada yapıyoruz.
            //modelBuilder.Configurations.Add(new ProductMAP());
            //modelBuilder.Configurations.Add(new CategoryMAP());
            //modelBuilder.Configurations.Add(new UserMAP());

            // Tek tek eklemek yerine Assembly'den eklenebilirler.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
