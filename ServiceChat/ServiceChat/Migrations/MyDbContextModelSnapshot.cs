﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceChat.Data;

namespace ServiceChat.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("ServiceChat.Model.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Administrador")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Estado")
                        .HasColumnType("text");

                    b.Property<int>("Id_Sala")
                        .HasColumnType("int");

                    b.Property<int>("Id_Utilizador")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Id_Sala");

                    b.HasIndex("Id_Utilizador");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("ServiceChat.Model.Mensagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data_Mensagem")
                        .HasColumnType("datetime");

                    b.Property<string>("Ficheiro")
                        .HasColumnType("text");

                    b.Property<int>("Id_Sala")
                        .HasColumnType("int");

                    b.Property<int>("Id_Utilizador")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id_Sala");

                    b.HasIndex("Id_Utilizador");

                    b.ToTable("Mensagem");
                });

            modelBuilder.Entity("ServiceChat.Model.Sala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("isAtiva")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("ServiceChat.Model.Utilizador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("text");

                    b.Property<string>("Funcao")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("Numero_Aluno")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Utilizador");
                });

            modelBuilder.Entity("ServiceChat.Model.Grupo", b =>
                {
                    b.HasOne("ServiceChat.Model.Sala", "Sala")
                        .WithMany("Grupo")
                        .HasForeignKey("Id_Sala")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceChat.Model.Utilizador", "Utilizador")
                        .WithMany("Grupo")
                        .HasForeignKey("Id_Utilizador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sala");

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("ServiceChat.Model.Mensagem", b =>
                {
                    b.HasOne("ServiceChat.Model.Sala", "Sala")
                        .WithMany("Mensagem")
                        .HasForeignKey("Id_Sala")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceChat.Model.Utilizador", "Utilizador")
                        .WithMany("Mensagem")
                        .HasForeignKey("Id_Utilizador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sala");

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("ServiceChat.Model.Sala", b =>
                {
                    b.Navigation("Grupo");

                    b.Navigation("Mensagem");
                });

            modelBuilder.Entity("ServiceChat.Model.Utilizador", b =>
                {
                    b.Navigation("Grupo");

                    b.Navigation("Mensagem");
                });
#pragma warning restore 612, 618
        }
    }
}
