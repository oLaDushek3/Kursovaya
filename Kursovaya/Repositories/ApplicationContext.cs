using System.Collections.Generic;
using Kursovaya.Model.Buyer;
using Kursovaya.Model.Factory;
using Kursovaya.Model.Place;
using Kursovaya.Model.Product;
using Kursovaya.Model.Shipping;
using Kursovaya.Model.Supply;
using Kursovaya.Model.User;
using Kursovaya.Model.Worker;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya.Repositories;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BuyerModel> Buyers { get; set; }

    public virtual DbSet<BuyerAddressModel> BuyerAddresses { get; set; }

    public virtual DbSet<FactoryModel> Factories { get; set; }

    public virtual DbSet<IndividualModel> Individuals { get; set; }

    public virtual DbSet<LegalEntityModel> LegalEntities { get; set; }

    public virtual DbSet<PlaceModel> Places { get; set; }

    public virtual DbSet<PostModel> Posts { get; set; }

    public virtual DbSet<ProductModel> Products { get; set; }

    public virtual DbSet<ProductTypeModel> ProductTypes { get; set; }

    public virtual DbSet<ProductsGroupModel> ProductsGroups { get; set; }

    public virtual DbSet<ShippingModel> Shippings { get; set; }

    public virtual DbSet<ShippingProductModel> ShippingProducts { get; set; }

    public virtual DbSet<ShippingProductPlaceModel> ShippingProductPlaces { get; set; }

    public virtual DbSet<SupplyModel> Supplies { get; set; }

    public virtual DbSet<SupplyProductModel> SupplyProducts { get; set; }

    public virtual DbSet<SupplyProductPlaceModel> SupplyProductPlaces { get; set; }

    public virtual DbSet<UserModel> Users { get; set; }

    public virtual DbSet<WorkerModel> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Server=.;Database=Kursovaya;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuyerModel>(entity =>
        {
            entity.HasKey(e => e.Buyer1).HasName("PK__Buyer__6DF4EAAC1A354894");

            entity.ToTable("Buyer");

            entity.HasIndex(e => e.Buyer1, "UQ__Buyer__6DF4EAADAA806492").IsUnique();

            entity.Property(e => e.Buyer1).HasColumnName("Buyer");
            entity.Property(e => e.IndividualId).HasColumnName("Individual_id");
            entity.Property(e => e.LegalEntityId).HasColumnName("Legal_entity_id");

            entity.HasOne(d => d.Individual).WithMany(p => p.Buyers)
                .HasForeignKey(d => d.IndividualId)
                .HasConstraintName("FK__Buyer__Individua__2077C861").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.LegalEntity).WithMany(p => p.Buyers)
                .HasForeignKey(d => d.LegalEntityId)
                .HasConstraintName("FK__Buyer__Legal_ent__1E8F7FEF").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BuyerAddressModel>(entity =>
        {
            entity.HasKey(e => e.BuyerAddressId).HasName("PK__Buyer_ad__44F7A337C129B681");

            entity.ToTable("Buyer_address");

            entity.HasIndex(e => e.BuyerAddressId, "UQ__Buyer_ad__44F7A336DA66FE7A").IsUnique();

            entity.Property(e => e.BuyerAddressId).HasColumnName("Buyer_address_id");
            entity.Property(e => e.Adress)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.IndividualId).HasColumnName("Individual_id");
            entity.Property(e => e.LegalEntityId).HasColumnName("Legal_entity_id");
            entity.Property(e => e.Note)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Individual).WithMany(p => p.BuyerAddresses)
                .HasForeignKey(d => d.IndividualId)
                .HasConstraintName("FK__Buyer_add__Indiv__216BEC9A").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.LegalEntity).WithMany(p => p.BuyerAddresses)
                .HasForeignKey(d => d.LegalEntityId)
                .HasConstraintName("FK__Buyer_add__Legal__1F83A428").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FactoryModel>(entity =>
        {
            entity.HasKey(e => e.FactoryId).HasName("PK__Factory__7956E0C8CE19319F");

            entity.ToTable("Factory");

            entity.HasIndex(e => e.FactoryId, "UQ__Factory__7956E0C9FDF1978C").IsUnique();

            entity.Property(e => e.FactoryId).HasColumnName("Factory_id");
            entity.Property(e => e.Address)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IndividualModel>(entity =>
        {
            entity.HasKey(e => e.IndividualId).HasName("PK__Individu__FA21AA76F82D85AC");

            entity.ToTable("Individual");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Individu__17A35CA42B2BF4B9").IsUnique();

            entity.HasIndex(e => e.SeriesPassportNumber, "UQ__Individu__A193672625341B5E").IsUnique();

            entity.HasIndex(e => e.IndividualId, "UQ__Individu__FA21AA774AAB8B32").IsUnique();

            entity.Property(e => e.IndividualId).HasColumnName("Individual_id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.SeriesPassportNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Series_passport_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<LegalEntityModel>(entity =>
        {
            entity.HasKey(e => e.LegalEntityId).HasName("PK__Legal_en__B6F64F8286D4A840");

            entity.ToTable("Legal_entity");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Legal_en__7E87EC6750CCAB95").IsUnique();

            entity.HasIndex(e => e.LegalEntityId, "UQ__Legal_en__B6F64F836E855001").IsUnique();

            entity.Property(e => e.LegalEntityId).HasColumnName("Legal_entity_id");
            entity.Property(e => e.Bank)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Bic)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("BIC");
            entity.Property(e => e.CorrespondentAccount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Correspondent_account");
            entity.Property(e => e.Organization)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("organization");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Phone_number");
            entity.Property(e => e.Rrc)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("RRC");
            entity.Property(e => e.Tin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TIN");
            entity.Property(e => e.СheckingAccount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Сhecking_account");
        });

        modelBuilder.Entity<PlaceModel>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__Place__D5CC0774B65E03E3");

            entity.ToTable("Place");

            entity.HasIndex(e => e.Place1, "UQ__Place__8310F9990143A48B").IsUnique();

            entity.HasIndex(e => e.PlaceId, "UQ__Place__D5CC0775D87CE856").IsUnique();

            entity.Property(e => e.PlaceId).HasColumnName("Place_id");
            entity.Property(e => e.Place1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Place");
        });

        modelBuilder.Entity<PostModel>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__587ADB65D60454AA");

            entity.ToTable("Post");

            entity.HasIndex(e => e.PostId, "UQ__Post__587ADB64343E9A04").IsUnique();

            entity.Property(e => e.PostId).HasColumnName("Post_id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__9833FF92A601A94D");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductId, "UQ__Product__9833FF93C00741CA").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.Characteristic)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PricePerUnit)
                .HasColumnType("money")
                .HasColumnName("Price_per_unit");
            entity.Property(e => e.ProductTypeId).HasColumnName("Product_type_id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => new { d.ProductTypeId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__1CA7377D").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductTypeModel>(entity =>
        {
            entity.HasKey(e => new { e.ProductTypeId }).HasName("PK__Product___F4FDAA1275197FCF");

            entity.ToTable("Product_type");

            entity.HasIndex(e => e.ProductTypeId, "UQ__Product___B25352FF01CAB5F4").IsUnique();

            entity.Property(e => e.ProductTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Product_type_id");
            entity.Property(e => e.ProductsGroupId).HasColumnName("Products_group_Id");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductsGroup).WithMany(p => p.ProductTypes)
                .HasForeignKey(d => d.ProductsGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product_t__Produ__15FA39EE").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductsGroupModel>(entity =>
        {
            entity.HasKey(e => e.ProductsGroupId).HasName("PK__Products__6AEF8EC5F38C6B40");

            entity.ToTable("Products_group");

            entity.HasIndex(e => e.ProductsGroupId, "UQ__Products__6AEF8EC411DE1312").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Products__737584F67E1559D5").IsUnique();

            entity.Property(e => e.ProductsGroupId).HasColumnName("Products_group_Id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ShippingModel>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("PK__Shipping__D9BC44985698E2AA");

            entity.ToTable("Shipping");

            entity.HasIndex(e => e.ShippingId, "UQ__Shipping__D9BC449990F1C063").IsUnique();

            entity.Property(e => e.ShippingId).HasColumnName("Shipping_id");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.BuyerNavigation).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.Buyer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shipping__Buyer__1D9B5BB6").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ShippingProductModel>(entity =>
        {
            entity.HasKey(e => e.ShippingProductId).HasName("PK__Shipping__DAE77F06640843DB");

            entity.ToTable("Shipping_Product");

            entity.HasIndex(e => e.ShippingProductId, "UQ__Shipping__DAE77F07F7F22C2B").IsUnique();

            entity.Property(e => e.ShippingProductId).HasColumnName("Shipping_Product_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.ShippingId).HasColumnName("Shipping_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ShippingProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shipping___Produ__150615B5").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Shipping).WithMany(p => p.ShippingProducts)
                .HasForeignKey(d => d.ShippingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shipping___Shipp__1229A90A").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ShippingProductPlaceModel>(entity =>
        {
            entity.HasKey(e => new { e.ShippingProductId, e.PlaceId }).HasName("PK__Shipping__87BBBF7129CC9E32");

            entity.ToTable("Shipping_Product_Place");

            entity.Property(e => e.ShippingProductId).HasColumnName("Shipping_Product_id");
            entity.Property(e => e.PlaceId).HasColumnName("Place_id");

            entity.HasOne(d => d.Place).WithMany(p => p.ShippingProductPlaces)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shipping___Place__2354350C").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ShippingProduct).WithMany(p => p.ShippingProductPlaces)
                .HasForeignKey(d => d.ShippingProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shipping___Shipp__19CACAD2").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SupplyModel>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("PK__Supply__D53AD15AA9F23A39");

            entity.ToTable("Supply");

            entity.HasIndex(e => e.SupplyId, "UQ__Supply__D53AD15BDE134DC7").IsUnique();

            entity.Property(e => e.SupplyId).HasColumnName("Supply_id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FactoryId).HasColumnName("Factory_id");

            entity.HasOne(d => d.Factory).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.FactoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Supply__Factory___18D6A699").OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(d => d.Workers).WithMany(p => p.Supplies)
                .UsingEntity<Dictionary<string, object>>(
                    "SupplyWorker",
                    r => r.HasOne<WorkerModel>().WithMany()
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Supply_Wo__Worke__16EE5E27").OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne<SupplyModel>().WithMany()
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Supply_Wo__Suppl__113584D1").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("SupplyId", "WorkerId").HasName("PK__Supply_W__0A0F08E567334934");
                        j.ToTable("Supply_Worker");
                        j.IndexerProperty<int>("SupplyId").HasColumnName("Supply_id");
                        j.IndexerProperty<int>("WorkerId").HasColumnName("Worker_id");
                    });
        });

        modelBuilder.Entity<SupplyProductModel>(entity =>
        {
            entity.HasKey(e => e.SupplyProductId).HasName("PK__Supply_P__F64292D6D68C9FF2");

            entity.ToTable("Supply_Product");

            entity.HasIndex(e => e.SupplyProductId, "UQ__Supply_P__F64292D70A39565E").IsUnique();

            entity.Property(e => e.SupplyProductId).HasColumnName("Supply_Product_id");
            entity.Property(e => e.ProductId).HasColumnName("Product_id");
            entity.Property(e => e.SupplyId).HasColumnName("Supply_id");

            entity.HasOne(d => d.Product).WithMany(p => p.SupplyProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Supply_Pr__Produ__1411F17C").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Supply).WithMany(p => p.SupplyProducts)
                .HasForeignKey(d => d.SupplyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Supply_Pr__Suppl__10416098").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SupplyProductPlaceModel>(entity =>
        {
            entity.HasKey(e => new { e.SupplyProductId, e.PlaceId }).HasName("PK__Supply_P__AB1E52A1D362F28F");

            entity.ToTable("Supply_Product_Place");

            entity.Property(e => e.SupplyProductId).HasColumnName("Supply_Product_id");
            entity.Property(e => e.PlaceId).HasColumnName("Place_id");

            entity.HasOne(d => d.Place).WithMany(p => p.SupplyProductPlaces)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Supply_Pr__Place__226010D3").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.SupplyProduct).WithMany(p => p.SupplyProductPlaces)
                .HasForeignKey(d => d.SupplyProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Supply_Pr__Suppl__1ABEEF0B").OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User");

            entity.HasIndex(e => e.UserId, "UQ__User__206A9DF933D24164").IsUnique();

            entity.HasIndex(e => e.Login, "UQ__User__5E55825B332C6172").IsUnique();

            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("User_id");
        });

        modelBuilder.Entity<WorkerModel>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("PK__Worker__F35D9BFCCE269F08");

            entity.ToTable("Worker");

            entity.HasIndex(e => e.FullName, "UQ__Worker__1949139001FA40C6").IsUnique();

            entity.HasIndex(e => e.WorkerId, "UQ__Worker__F35D9BFD3E1D9020").IsUnique();

            entity.Property(e => e.WorkerId).HasColumnName("Worker_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("Full_name");
            entity.Property(e => e.PostId).HasColumnName("Post_id");
            entity.Property(e => e.TypeOfContract)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Type_of_contract");
            entity.Property(e => e.TypeOfSalary)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Type_of_salary");

            entity.HasOne(d => d.Post).WithMany(p => p.Workers)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Worker__Post_id__1BB31344").OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(d => d.Shippings).WithMany(p => p.Workers)
                .UsingEntity<Dictionary<string, object>>(
                    "ShippingWorker",
                    r => r.HasOne<ShippingModel>().WithMany()
                        .HasForeignKey("ShippingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Shipping___Shipp__131DCD43").OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne<WorkerModel>().WithMany()
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Shipping___Worke__17E28260").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("WorkerId", "ShippingId").HasName("PK__Shipping__6EC65FB5900DCB1E");
                        j.ToTable("Shipping_Worker");
                        j.IndexerProperty<int>("WorkerId").HasColumnName("Worker_id");
                        j.IndexerProperty<int>("ShippingId").HasColumnName("Shipping_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
