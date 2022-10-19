﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPrestamos.DAL;

#nullable disable

namespace RPrestamos.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20221017214147_RegistroPrestamo")]
    partial class RegistroPrestamo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("RPrestamos.Entidades.Ocupaciones", b =>
                {
                    b.Property<int>("OcupacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Salario")
                        .HasColumnType("REAL");

                    b.HasKey("OcupacionId");

                    b.ToTable("Ocupaciones");
                });

            modelBuilder.Entity("RPrestamos.Entidades.Pago", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Concepto")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<float>("Monto")
                        .HasColumnType("REAL");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PagoId");

                    b.ToTable("Pago");
                });

            modelBuilder.Entity("RPrestamos.Entidades.PagosDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PagoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PrestamoId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ValorPagado")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PagoId");

                    b.ToTable("PagosDetalle");
                });

            modelBuilder.Entity("RPrestamos.Entidades.Personas", b =>
                {
                    b.Property<int>("PersonaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Balance")
                        .HasColumnType("REAL");

                    b.Property<double>("Celular")
                        .HasColumnType("REAL");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OcupacionId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Telefono")
                        .HasColumnType("REAL");

                    b.HasKey("PersonaId");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("RPrestamos.Entidades.Prestamos", b =>
                {
                    b.Property<int>("PrestamosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Concepto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<float>("Monto")
                        .HasColumnType("REAL");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Vence")
                        .HasColumnType("INTEGER");

                    b.HasKey("PrestamosId");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("RPrestamos.Entidades.PagosDetalle", b =>
                {
                    b.HasOne("RPrestamos.Entidades.Pago", null)
                        .WithMany("PagosDetalle")
                        .HasForeignKey("PagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RPrestamos.Entidades.Pago", b =>
                {
                    b.Navigation("PagosDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
