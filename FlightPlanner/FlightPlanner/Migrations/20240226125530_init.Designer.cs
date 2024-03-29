﻿// <auto-generated />
using FlightPlanner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightPlanner.Migrations
{
    [DbContext(typeof(FlightPlannerDbContext))]
    [Migration("20240226125530_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlightPlanner.Modules.Airport", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("AirportCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "airport");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("FlightPlanner.Modules.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArrivalTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Carrier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Fromid")
                        .HasColumnType("int");

                    b.Property<int>("Toid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Fromid");

                    b.HasIndex("Toid");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightPlanner.Modules.Flight", b =>
                {
                    b.HasOne("FlightPlanner.Modules.Airport", "From")
                        .WithMany()
                        .HasForeignKey("Fromid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightPlanner.Modules.Airport", "To")
                        .WithMany()
                        .HasForeignKey("Toid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });
#pragma warning restore 612, 618
        }
    }
}
