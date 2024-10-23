﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Setting_Api.Data.Context;

#nullable disable

namespace Setting_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241023134319_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Setting_Api.Model.Entity.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CasinoId")
                        .HasColumnType("int");

                    b.Property<int>("GamingDateTurnHour")
                        .HasColumnType("int");

                    b.Property<int>("GamingDateTurnMinute")
                        .HasColumnType("int");

                    b.Property<int>("TimeZone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("settings", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}