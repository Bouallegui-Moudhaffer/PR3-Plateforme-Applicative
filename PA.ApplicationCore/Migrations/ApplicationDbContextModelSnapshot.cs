﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PA.ApplicationCore;

#nullable disable

namespace PA.ApplicationCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PA.ApplicationCore.Domain.Etablissement", b =>
                {
                    b.Property<int>("EtablissementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EtablissementId"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EtablissementId");

                    b.HasIndex("StatusId");

                    b.ToTable("Etablissements");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EtablissementId")
                        .HasColumnType("int");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostesId")
                        .HasColumnType("int");

                    b.Property<int?>("SallesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("LogId");

                    b.HasIndex("EtablissementId");

                    b.HasIndex("PostesId");

                    b.HasIndex("SallesId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Postes", b =>
                {
                    b.Property<int>("PostesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostesId"));

                    b.Property<float?>("CpuUsageMean")
                        .HasColumnType("real");

                    b.Property<float?>("CpuUsageMedian")
                        .HasColumnType("real");

                    b.Property<float?>("CpuUsagePeak")
                        .HasColumnType("real");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MacAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("MemoryUsageMean")
                        .HasColumnType("real");

                    b.Property<float?>("MemoryUsageMedian")
                        .HasColumnType("real");

                    b.Property<float?>("MemoryUsagePeak")
                        .HasColumnType("real");

                    b.Property<string>("Ref")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SallesId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("derniereMaintenance")
                        .HasColumnType("datetime2");

                    b.HasKey("PostesId");

                    b.HasIndex("SallesId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TypeId");

                    b.ToTable("Postes");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Salles", b =>
                {
                    b.Property<int>("SallesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SallesId"));

                    b.Property<int>("Capacite")
                        .HasColumnType("int");

                    b.Property<int>("EtablissementId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("SallesId");

                    b.HasIndex("EtablissementId");

                    b.HasIndex("StatusId");

                    b.ToTable("Salles");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Object")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Types", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Object")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TypeId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UtilisateurId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UtilisateurId");

                    b.HasIndex("StatusId");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Etablissement", b =>
                {
                    b.HasOne("PA.ApplicationCore.Domain.Status", null)
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Log", b =>
                {
                    b.HasOne("PA.ApplicationCore.Domain.Etablissement", "Etablissement")
                        .WithMany()
                        .HasForeignKey("EtablissementId");

                    b.HasOne("PA.ApplicationCore.Domain.Postes", "Poste")
                        .WithMany()
                        .HasForeignKey("PostesId");

                    b.HasOne("PA.ApplicationCore.Domain.Salles", "Salle")
                        .WithMany()
                        .HasForeignKey("SallesId");

                    b.HasOne("PA.ApplicationCore.Domain.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId");

                    b.Navigation("Etablissement");

                    b.Navigation("Poste");

                    b.Navigation("Salle");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Postes", b =>
                {
                    b.HasOne("PA.ApplicationCore.Domain.Salles", null)
                        .WithMany("Postes")
                        .HasForeignKey("SallesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PA.ApplicationCore.Domain.Status", null)
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PA.ApplicationCore.Domain.Types", null)
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Salles", b =>
                {
                    b.HasOne("PA.ApplicationCore.Domain.Etablissement", null)
                        .WithMany("Salles")
                        .HasForeignKey("EtablissementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PA.ApplicationCore.Domain.Status", null)
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Utilisateur", b =>
                {
                    b.HasOne("PA.ApplicationCore.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Etablissement", b =>
                {
                    b.Navigation("Salles");
                });

            modelBuilder.Entity("PA.ApplicationCore.Domain.Salles", b =>
                {
                    b.Navigation("Postes");
                });
#pragma warning restore 612, 618
        }
    }
}
