﻿// <auto-generated />
using System;
using CarControl.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarControl.Infrastructure.Migrations
{
    [DbContext(typeof(CarControlContext))]
    [Migration("20230812193032_Incial")]
    partial class Incial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CarControl.Domain.Movimento", b =>
                {
                    b.Property<int>("IdMovimento")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdVeiculo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DtEntrada")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DtSaida")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("HrEntrada")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("HrSaida")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IdTpOperacao1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdVaga1")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdMovimento", "IdVeiculo");

                    b.HasIndex("IdTpOperacao1");

                    b.HasIndex("IdVaga1");

                    b.ToTable("Movimento");
                });

            modelBuilder.Entity("CarControl.Domain.Operacao", b =>
                {
                    b.Property<int>("IdTpOperacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("nmOperacao")
                        .HasColumnType("TEXT");

                    b.HasKey("IdTpOperacao");

                    b.ToTable("Operacao");
                });

            modelBuilder.Entity("CarControl.Domain.Vaga", b =>
                {
                    b.Property<int>("IdVaga")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NmVaga")
                        .HasColumnType("TEXT");

                    b.Property<char>("flVaga")
                        .HasColumnType("TEXT");

                    b.HasKey("IdVaga");

                    b.ToTable("Vaga");
                });

            modelBuilder.Entity("CarControl.Domain.Veiculo", b =>
                {
                    b.Property<int>("IdVeiculo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cor")
                        .HasColumnType("TEXT");

                    b.Property<int>("CpfCondutor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Marca")
                        .HasColumnType("TEXT");

                    b.Property<string>("Modelo")
                        .HasColumnType("TEXT");

                    b.Property<string>("NmCondutor")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlacaVeiculo")
                        .HasColumnType("TEXT");

                    b.Property<string>("TpVeiculo")
                        .HasColumnType("TEXT");

                    b.HasKey("IdVeiculo");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("CarControl.Domain.Movimento", b =>
                {
                    b.HasOne("CarControl.Domain.Operacao", "IdTpOperacao")
                        .WithMany()
                        .HasForeignKey("IdTpOperacao1");

                    b.HasOne("CarControl.Domain.Vaga", "IdVaga")
                        .WithMany()
                        .HasForeignKey("IdVaga1");

                    b.Navigation("IdTpOperacao");

                    b.Navigation("IdVaga");
                });
#pragma warning restore 612, 618
        }
    }
}