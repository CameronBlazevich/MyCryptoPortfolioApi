﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MyCryptoPortfolio;
using System;

namespace MyCryptoPortfolio.Migrations
{
    [DbContext(typeof(PortfolioContext))]
    partial class PortfolioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyCryptoPortfolio.Models.Holding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AmountOwned");

                    b.Property<string>("CoinTickerSymbol");

                    b.Property<int>("PortfolioId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Holdings");
                });

            modelBuilder.Entity("MyCryptoPortfolio.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("MyCryptoPortfolio.Models.Holding", b =>
                {
                    b.HasOne("MyCryptoPortfolio.Models.Portfolio")
                        .WithMany("Holdings")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
