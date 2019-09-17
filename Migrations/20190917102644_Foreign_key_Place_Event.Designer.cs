﻿// <auto-generated />
using System;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventApp.Migrations
{
    [DbContext(typeof(EventAppDbContext))]
    [Migration("20190917102644_Foreign_key_Place_Event")]
    partial class Foreign_key_Place_Event
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventApp.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("End");

                    b.Property<string>("Name");

                    b.Property<int>("PlaceIdentity");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasIndex("PlaceIdentity");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventApp.Models.Invitation", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("PersonId");

                    b.Property<int>("Status");

                    b.HasKey("EventId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("EventApp.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name", "DateOfBirth");

                    b.ToTable("People");
                });

            modelBuilder.Entity("EventApp.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("EventApp.Models.Event", b =>
                {
                    b.HasOne("EventApp.Models.Place", "Place")
                        .WithMany("Events")
                        .HasForeignKey("PlaceIdentity")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EventApp.Models.Invitation", b =>
                {
                    b.HasOne("EventApp.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventApp.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
