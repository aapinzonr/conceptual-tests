﻿// <auto-generated />
using System;
using EF.CodeFirstDemo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EF.CodeFirstDemo.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    [Migration("20230608221150_RefreshBuyTable")]
    partial class RefreshBuyTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EF.CodeFirstDemo.Models.DemoBuy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientDocument")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DemoBuy");
                });

            modelBuilder.Entity("EF.CodeFirstDemo.Models.DemoBuyItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DemoBuyId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DemoBuyId");

                    b.HasIndex("ProductId");

                    b.ToTable("DemoBuyItem");
                });

            modelBuilder.Entity("EF.CodeFirstDemo.Models.DemoProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<int>("InInventory")
                        .HasColumnType("int");

                    b.Property<int>("MaxQuantity")
                        .HasColumnType("int");

                    b.Property<int>("MinQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DemoProduct");
                });

            modelBuilder.Entity("EF.CodeFirstDemo.Models.DemoBuyItem", b =>
                {
                    b.HasOne("EF.CodeFirstDemo.Models.DemoBuy", null)
                        .WithMany("Products")
                        .HasForeignKey("DemoBuyId");

                    b.HasOne("EF.CodeFirstDemo.Models.DemoProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EF.CodeFirstDemo.Models.DemoBuy", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
