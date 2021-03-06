﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CrmHub.Infra.Data.Context;

namespace CrmHub.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrmHub.Domain.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("CrmHub.Domain.Models.Crm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Environment")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlAccount")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UrlService")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Crm");
                });

            modelBuilder.Entity("CrmHub.Domain.Models.LogApi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Crm")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Empresa")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Parameters")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Response");

                    b.Property<string>("Send")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("LogApi");
                });
        }
    }
}
