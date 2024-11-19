using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demo1111.Models;

namespace demo1111.Context;

public partial class MyprojContext : DbContext
{
    public MyprojContext()
    {
    }

    public MyprojContext(DbContextOptions<MyprojContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<EdIzm> EdIzms { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Prod> Prods { get; set; }

    public virtual DbSet<Punkt> Punkts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Zakaz> Zakazs { get; set; }

    public virtual DbSet<ZakazProd> ZakazProds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=myproj;Username=postgres;Password=18b22M02a");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCateg).HasName("category_pk");

            entity.ToTable("category", "demo1710");

            entity.Property(e => e.IdCateg)
                .ValueGeneratedNever()
                .HasColumnName("id_categ");
            entity.Property(e => e.Categ)
                .HasColumnType("character varying")
                .HasColumnName("categ");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("customer_pk");

            entity.ToTable("customer", "demo1710");

            entity.Property(e => e.IdCustomer)
                .ValueGeneratedNever()
                .HasColumnName("id_customer");
            entity.Property(e => e.Customer1)
                .HasColumnType("character varying")
                .HasColumnName("customer");
        });

        modelBuilder.Entity<EdIzm>(entity =>
        {
            entity.HasKey(e => e.IdEdIzm).HasName("ed_izm_pk");

            entity.ToTable("ed_izm", "demo1710");

            entity.Property(e => e.IdEdIzm)
                .ValueGeneratedNever()
                .HasColumnName("id_ed_izm");
            entity.Property(e => e.EdIzm1)
                .HasColumnType("character varying")
                .HasColumnName("ed_izm");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManuf).HasName("manufacturer_pk");

            entity.ToTable("manufacturer", "demo1710");

            entity.Property(e => e.IdManuf)
                .ValueGeneratedNever()
                .HasColumnName("id_manuf");
            entity.Property(e => e.Manuf)
                .HasColumnType("character varying")
                .HasColumnName("manuf");
        });

        modelBuilder.Entity<Prod>(entity =>
        {
            entity.HasKey(e => e.IdProd).HasName("prod_pk");

            entity.ToTable("prod", "demo1710");

            entity.Property(e => e.IdProd)
                .ValueGeneratedNever()
                .HasColumnName("id_prod");
            entity.Property(e => e.Articul)
                .HasColumnType("character varying")
                .HasColumnName("articul");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.CurrDisc).HasColumnName("curr_disc");
            entity.Property(e => e.Descr)
                .HasColumnType("character varying")
                .HasColumnName("descr");
            entity.Property(e => e.IdCateg).HasColumnName("id_categ");
            entity.Property(e => e.IdCust).HasColumnName("id_cust");
            entity.Property(e => e.IdEdIzm).HasColumnName("id_ed_izm");
            entity.Property(e => e.IdManuf).HasColumnName("id_manuf");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.MaxDisc).HasColumnName("max_disc");
            entity.Property(e => e.NameProd)
                .HasColumnType("character varying")
                .HasColumnName("name_prod");
            entity.Property(e => e.QuanSkl).HasColumnName("quan_skl");

            entity.HasOne(d => d.IdCategNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdCateg)
                .HasConstraintName("prod_category_fk");

            entity.HasOne(d => d.IdCustNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdCust)
                .HasConstraintName("prod_customer_fk");

            entity.HasOne(d => d.IdEdIzmNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdEdIzm)
                .HasConstraintName("prod_ed_izm_fk");

            entity.HasOne(d => d.IdManufNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdManuf)
                .HasConstraintName("prod_manufacturer_fk");
        });

        modelBuilder.Entity<Punkt>(entity =>
        {
            entity.HasKey(e => e.IdPunkt).HasName("punkt_pk");

            entity.ToTable("punkt", "demo1710");

            entity.Property(e => e.IdPunkt)
                .ValueGeneratedNever()
                .HasColumnName("id_punkt");
            entity.Property(e => e.Punkt1)
                .HasColumnType("character varying")
                .HasColumnName("punkt");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("roles_pk");

            entity.ToTable("roles", "demo1710");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Role1)
                .HasColumnType("character varying")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("status_pk");

            entity.ToTable("status", "demo1710");

            entity.Property(e => e.IdStatus)
                .ValueGeneratedNever()
                .HasColumnName("id_status");
            entity.Property(e => e.Status1)
                .HasColumnType("character varying")
                .HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("user_pk");

            entity.ToTable("user", "demo1710");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("user_roles_fk");
        });

        modelBuilder.Entity<Zakaz>(entity =>
        {
            entity.HasKey(e => e.IdZakaz).HasName("zakaz_pk");

            entity.ToTable("zakaz", "demo1710");

            entity.Property(e => e.IdZakaz).HasColumnName("id_zakaz");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.DateCr).HasColumnName("date_cr");
            entity.Property(e => e.DateDeliver).HasColumnName("date_deliver");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdPunkt).HasColumnName("id_punkt");
            entity.Property(e => e.IdStatus).HasColumnName("id_status");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Zakazs)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("zakaz_user_fk");

            entity.HasOne(d => d.IdPunktNavigation).WithMany(p => p.Zakazs)
                .HasForeignKey(d => d.IdPunkt)
                .HasConstraintName("zakaz_punkt_fk");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Zakazs)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("zakaz_status_fk");
        });

        modelBuilder.Entity<ZakazProd>(entity =>
        {
            entity.HasKey(e => new { e.IdZakaz, e.IdProd }).HasName("zakaz_prod_pk");

            entity.ToTable("zakaz_prod", "demo1710");

            entity.Property(e => e.IdZakaz)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_zakaz");
            entity.Property(e => e.IdProd)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_prod");
            entity.Property(e => e.Amount).HasColumnName("amount");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.ZakazProds)
                .HasForeignKey(d => d.IdProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("zakaz_prod_prod_fk");

            entity.HasOne(d => d.IdZakazNavigation).WithMany(p => p.ZakazProds)
                .HasForeignKey(d => d.IdZakaz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("zakaz_prod_zakaz_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
