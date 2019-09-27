﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Models;

namespace ServerApi.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20190924162604_remove-required-publisher")]
    partial class removerequiredpublisher
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Copies");

                    b.Property<int>("CopyNumber");

                    b.Property<int>("Discount");

                    b.Property<Guid>("ISBN");

                    b.Property<bool>("IsActive");

                    b.Property<double>("Price");

                    b.Property<double>("PriceAfter");

                    b.Property<DateTime>("PrintDate");

                    b.Property<string>("Publisher");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Topic");

                    b.Property<string>("Type");

                    b.Property<int>("categoryBook");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Models.Models.Jornal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CopyNumber");

                    b.Property<int>("Discount");

                    b.Property<Guid>("ISBN");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Month")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<double>("PriceAfter");

                    b.Property<DateTime>("PrintDate");

                    b.Property<string>("Publisher");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Topic");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Jornals");
                });

            modelBuilder.Entity("Models.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
