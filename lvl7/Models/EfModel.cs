using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace lvl7.Models
{
    public partial class EfModel : DbContext
    {


        public static EfModel Instance;
        public static EfModel Init()
        {
            if (Instance == null)
                Instance = new EfModel();
            return Instance;
        }
        public EfModel()
        {
        }

        public static bool Save()
        {
            try
            {
                EfModel.Init().SaveChanges();
                return true;
            } catch (Exception ex)
            {
                MessageBox.Show("Не все поля заполнены правильно", "Ошибка");
                Instance = null;
                return false;
            }
        }

        public EfModel(DbContextOptions<EfModel> options)
            : base(options)
        {
        }

        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<MaterialHasProduct> MaterialHasProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=cfif31.ru;database=ISPr23-47_BorovikovVN_mdk0202;user id=ISPr23-47_BorovikovVN;password=ISPr23-47_BorovikovVN", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.IdMaterial)
                    .HasName("PRIMARY");

                entity.ToTable("Material");

                entity.Property(e => e.IdMaterial).HasColumnName("idMaterial");

                entity.Property(e => e.MaterialName)
                    .HasMaxLength(45)
                    .HasColumnName("materialName");
            });

            modelBuilder.Entity<MaterialHasProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductIdProduct, e.MaterialIdMaterial })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("material_has_product");

                entity.HasIndex(e => e.MaterialIdMaterial, "fx_material_idx");

                entity.HasIndex(e => e.ProductIdProduct, "fx_product_idx");

                entity.Property(e => e.ProductIdProduct).HasColumnName("product_idProduct");

                entity.Property(e => e.MaterialIdMaterial).HasColumnName("material_idMaterial");

                entity.Property(e => e.Amount)
                    .HasMaxLength(45)
                    .HasColumnName("amount");

                entity.HasOne(d => d.MaterialIdMaterialNavigation)
                    .WithMany(p => p.MaterialHasProducts)
                    .HasForeignKey(d => d.MaterialIdMaterial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_material");

                entity.HasOne(d => d.ProductIdProductNavigation)
                    .WithMany(p => p.MaterialHasProducts)
                    .HasForeignKey(d => d.ProductIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PRIMARY");

                entity.ToTable("Product");

                entity.HasIndex(e => e.TypeProduct, "fx_productstype_idx");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.Article)
                    .HasMaxLength(45)
                    .HasColumnName("article")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.NameProduct)
                    .HasMaxLength(45)
                    .HasColumnName("nameProduct")
                    .HasDefaultValueSql("'a'");

                entity.Property(e => e.Photo)
                    .HasColumnType("blob")
                    .HasColumnName("photo");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TypeProduct).HasColumnName("typeProduct");

                entity.HasOne(d => d.TypeProductNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_productstype");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.IdProductTypes)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdProductTypes).HasColumnName("idProductTypes");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(45)
                    .HasColumnName("typeName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
