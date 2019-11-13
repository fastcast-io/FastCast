﻿// <auto-generated />
using System;
using FastCast.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastCast.Migrations
{
    [DbContext(typeof(FastCastContext))]
    [Migration("20191113020929_LatandLong")]
    partial class LatandLong
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("FormId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InitiatorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLive")
                        .HasColumnType("bit");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

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