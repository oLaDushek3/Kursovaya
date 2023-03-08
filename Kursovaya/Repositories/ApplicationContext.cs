using Kursovaya.Model;
using Kursovaya.Model.User;
using Kursovaya.Model.Worker;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya.Repositories
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Buyer> Buyer { get; set; }
        public virtual DbSet<Buyer_address> Buyer_address { get; set; }
        public virtual DbSet<Factory> Factory { get; set; }
        public virtual DbSet<Individual> Individual { get; set; }
        public virtual DbSet<Legal_entity> Legal_entity { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Product_type> Product_type { get; set; }
        public virtual DbSet<Products_group> Products_group { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<Shipping_Product> Shipping_Product { get; set; }
        public virtual DbSet<Shipping_Product_Place> Shipping_Product_Place { get; set; }
        public virtual DbSet<Supply> Supply { get; set; }
        public virtual DbSet<Supply_Product> Supply_Product { get; set; }
        public virtual DbSet<Supply_Product_Place> Supply_Product_Place { get; set; }
        public virtual DbSet<WorkerModel> Worker { get; set; }
        public virtual DbSet<UserModel> User { get; set; }

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0D2QSEB; Initial Catalog=Kursovaya;Integrated Security=True;TrustServerCertificate=true");
        }
    }
}
