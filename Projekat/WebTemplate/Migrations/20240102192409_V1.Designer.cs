﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240102192409_V1")]
    partial class V1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AktivnostPutovanje", b =>
                {
                    b.Property<int>("AktivnostiId")
                        .HasColumnType("int");

                    b.Property<int>("PutovanjeId")
                        .HasColumnType("int");

                    b.HasKey("AktivnostiId", "PutovanjeId");

                    b.HasIndex("PutovanjeId");

                    b.ToTable("AktivnostPutovanje");
                });

            modelBuilder.Entity("KorisnikRezervacija", b =>
                {
                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("RezervacijaId")
                        .HasColumnType("int");

                    b.HasKey("KorisnikId", "RezervacijaId");

                    b.HasIndex("RezervacijaId");

                    b.ToTable("KorisnikRezervacija");
                });

            modelBuilder.Entity("Models.Agencija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Agencije");
                });

            modelBuilder.Entity("Models.Aktivnost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Aktivnosti");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AgencijaId")
                        .HasColumnType("int");

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AgencijaId");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Models.Putovanje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AgencijaId")
                        .HasColumnType("int");

                    b.Property<int>("BrojNocenja")
                        .HasColumnType("int");

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<string>("Mesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prevoz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RezervacijaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgencijaId");

                    b.HasIndex("RezervacijaId");

                    b.ToTable("Putovanja");
                });

            modelBuilder.Entity("Models.Recenzija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Komentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("Ocena")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Recenzije");
                });

            modelBuilder.Entity("Models.Rezervacija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrojOsoba")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumDo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumOd")
                        .HasColumnType("datetime2");

                    b.Property<string>("Smestaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rezervacije");
                });

            modelBuilder.Entity("AktivnostPutovanje", b =>
                {
                    b.HasOne("Models.Aktivnost", null)
                        .WithMany()
                        .HasForeignKey("AktivnostiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Putovanje", null)
                        .WithMany()
                        .HasForeignKey("PutovanjeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KorisnikRezervacija", b =>
                {
                    b.HasOne("Models.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Rezervacija", null)
                        .WithMany()
                        .HasForeignKey("RezervacijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.HasOne("Models.Agencija", "Agencija")
                        .WithMany("Korisnici")
                        .HasForeignKey("AgencijaId");

                    b.Navigation("Agencija");
                });

            modelBuilder.Entity("Models.Putovanje", b =>
                {
                    b.HasOne("Models.Agencija", "Agencija")
                        .WithMany("Putovanje")
                        .HasForeignKey("AgencijaId");

                    b.HasOne("Models.Rezervacija", null)
                        .WithMany("Putovanje")
                        .HasForeignKey("RezervacijaId");

                    b.Navigation("Agencija");
                });

            modelBuilder.Entity("Models.Recenzija", b =>
                {
                    b.HasOne("Models.Korisnik", "Korisnik")
                        .WithMany("Recenzija")
                        .HasForeignKey("KorisnikId");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("Models.Agencija", b =>
                {
                    b.Navigation("Korisnici");

                    b.Navigation("Putovanje");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Navigation("Recenzija");
                });

            modelBuilder.Entity("Models.Rezervacija", b =>
                {
                    b.Navigation("Putovanje");
                });
#pragma warning restore 612, 618
        }
    }
}