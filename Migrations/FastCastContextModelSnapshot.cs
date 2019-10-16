﻿// <auto-generated />
using System;
using FastCast.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastCast.Migrations
{
    [DbContext(typeof(FastCastContext))]
    partial class FastCastContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FastCast.Models.Session", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<int>("InitiatorId")
                        .HasColumnType("int");

                    b.Property<string>("SessionCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Timer")
                        .HasColumnType("int");

                    b.HasKey("SessionId");

                    b.ToTable("Session");
                });
#pragma warning restore 612, 618
        }
    }
}
