﻿// <auto-generated />
using System;
using Fintacharts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fintacharts.Infrastructure.Migrations
{
    [DbContext(typeof(FintachartsDbContext))]
    partial class FintachartsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("AssetProvider", b =>
                {
                    b.Property<Guid>("AssetId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProvidersId")
                        .HasColumnType("TEXT");

                    b.HasKey("AssetId", "ProvidersId");

                    b.HasIndex("ProvidersId");

                    b.ToTable("AssetProvider");
                });

            modelBuilder.Entity("Fintacharts.Abstractions.Entities.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AssetKind")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Fintacharts.Abstractions.Entities.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("AssetProvider", b =>
                {
                    b.HasOne("Fintacharts.Abstractions.Entities.Asset", null)
                        .WithMany()
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fintacharts.Abstractions.Entities.Provider", null)
                        .WithMany()
                        .HasForeignKey("ProvidersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
