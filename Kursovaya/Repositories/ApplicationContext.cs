using Kursovaya.Model;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;
using Kursovaya.Model.User;
using Kursovaya.Model.Worker;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya.Repositories
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<BuyerModel> Buyer { get; set; }
        public virtual DbSet<Buyer_addressModel> Buyer_address { get; set; }
        public virtual DbSet<FactoryModel> Factory { get; set; }
        public virtual DbSet<IndividualModel> Individual { get; set; }
        public virtual DbSet<Legal_entityModel> Legal_entity { get; set; }
        public virtual DbSet<PlaceModel> Place { get; set; }
        public virtual DbSet<PostModel> Post { get; set; }
        public virtual DbSet<ProductModel> Product { get; set; }
        public virtual DbSet<Product_typeModel> Product_type { get; set; }
        public virtual DbSet<Product_groupModel> Products_group { get; set; }
        public virtual DbSet<ShippingModel> Shipping { get; set; }
        public virtual DbSet<Shipping_ProductModel> Shipping_Product { get; set; }
        public virtual DbSet<Shipping_Product_PlaceModel> Shipping_Product_Place { get; set; }
        public virtual DbSet<SupplyModel> Supply { get; set; }
        public virtual DbSet<Supply_ProductModel> Supply_Product { get; set; }
        public virtual DbSet<Supply_Product_PlaceModel> Supply_Product_Place { get; set; }
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
