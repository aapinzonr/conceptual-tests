﻿// <auto-generated />
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
    [Migration("20230608102850_CreateDemoDbProduct")]
    partial class CreateDemoDbProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
#pragma warning restore 612, 618
        }
    }
}