﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dal;

#nullable disable

namespace dal.Migrations
{
    [DbContext(typeof(Type2Context))]
    partial class Type2ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("models.Categorie", b =>
                {
                    b.Property<int>("CategorieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategorieId"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategorieId");

                    b.HasIndex("Naam")
                        .IsUnique();

                    b.ToTable("Categorieën");
                });

            modelBuilder.Entity("models.Dienst", b =>
                {
                    b.Property<int>("DienstId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DienstId"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("money");

                    b.HasKey("DienstId");

                    b.ToTable("Diensten");
                });

            modelBuilder.Entity("models.Factuur", b =>
                {
                    b.Property<int>("FactuurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FactuurId"));

                    b.Property<bool>("BtwNummer")
                        .HasColumnType("bit");

                    b.Property<int>("BtwPercentage")
                        .HasColumnType("int");

                    b.Property<int?>("KortingskaartId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("FactuurId");

                    b.HasIndex("KortingskaartId")
                        .IsUnique()
                        .HasFilter("[KortingskaartId] IS NOT NULL");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Facturen");
                });

            modelBuilder.Entity("models.Klant", b =>
                {
                    b.Property<int>("Klantid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Klantid"));

                    b.Property<string>("Achternaam")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Bedrijfsnaam")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Btwnummer")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Gemeente")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Huisnummer")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("Professioneel")
                        .HasColumnType("bit");

                    b.Property<string>("Straat")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Telefoonnummer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Voornaam")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Klantid");

                    b.ToTable("Klanten");
                });

            modelBuilder.Entity("models.Kortingskaart", b =>
                {
                    b.Property<int>("KortingskaartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KortingskaartId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<bool>("Professioneel")
                        .HasColumnType("bit");

                    b.Property<int>("Teller")
                        .HasColumnType("int");

                    b.HasKey("KortingskaartId");

                    b.ToTable("Kortingskaarten");
                });

            modelBuilder.Entity("models.Locatie", b =>
                {
                    b.Property<int>("LocatieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocatieId"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LocatieId");

                    b.ToTable("Locaties");
                });

            modelBuilder.Entity("models.Medewerker", b =>
                {
                    b.Property<int>("MedewerkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedewerkerId"));

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Paswoord")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MedewerkerId");

                    b.ToTable("Medewerkers");
                });

            modelBuilder.Entity("models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("KlantId")
                        .HasColumnType("int");

                    b.Property<string>("Ordernummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("KlantId");

                    b.HasIndex("Ordernummer")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("models.OrderDienst", b =>
                {
                    b.Property<int>("OrderDienstId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDienstId"));

                    b.Property<int>("DienstId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("OrderDienstId");

                    b.HasIndex("DienstId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDiensten");
                });

            modelBuilder.Entity("models.OrderProduct", b =>
                {
                    b.Property<int>("OrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderProductId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducten");
                });

            modelBuilder.Entity("models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("CategorieId")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("money");

                    b.Property<string>("Productnummer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategorieId");

                    b.HasIndex("Productnummer")
                        .IsUnique();

                    b.ToTable("Producten");
                });

            modelBuilder.Entity("models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockId"));

                    b.Property<int>("Aantal")
                        .HasColumnType("int");

                    b.Property<int>("LocatieId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("StockId");

                    b.HasIndex("LocatieId");

                    b.HasIndex("ProductId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("models.Factuur", b =>
                {
                    b.HasOne("models.Kortingskaart", "Kortingskaart")
                        .WithOne("Factuur")
                        .HasForeignKey("models.Factuur", "KortingskaartId");

                    b.HasOne("models.Order", "Order")
                        .WithOne("Factuur")
                        .HasForeignKey("models.Factuur", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kortingskaart");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("models.Order", b =>
                {
                    b.HasOne("models.Klant", "Klant")
                        .WithMany("Orders")
                        .HasForeignKey("KlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klant");
                });

            modelBuilder.Entity("models.OrderDienst", b =>
                {
                    b.HasOne("models.Dienst", "Dienst")
                        .WithMany()
                        .HasForeignKey("DienstId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dienst");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("models.OrderProduct", b =>
                {
                    b.HasOne("models.Order", "Order")
                        .WithMany("OrderProducten")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("models.Product", "Product")
                        .WithMany("OrderProducten")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("models.Product", b =>
                {
                    b.HasOne("models.Categorie", "Categorie")
                        .WithMany("Producten")
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");
                });

            modelBuilder.Entity("models.Stock", b =>
                {
                    b.HasOne("models.Locatie", "Locatie")
                        .WithMany("Stock")
                        .HasForeignKey("LocatieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("models.Product", "Product")
                        .WithMany("Stock")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locatie");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("models.Categorie", b =>
                {
                    b.Navigation("Producten");
                });

            modelBuilder.Entity("models.Klant", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("models.Kortingskaart", b =>
                {
                    b.Navigation("Factuur")
                        .IsRequired();
                });

            modelBuilder.Entity("models.Locatie", b =>
                {
                    b.Navigation("Stock");
                });

            modelBuilder.Entity("models.Order", b =>
                {
                    b.Navigation("Factuur")
                        .IsRequired();

                    b.Navigation("OrderProducten");
                });

            modelBuilder.Entity("models.Product", b =>
                {
                    b.Navigation("OrderProducten");

                    b.Navigation("Stock");
                });
#pragma warning restore 612, 618
        }
    }
}
