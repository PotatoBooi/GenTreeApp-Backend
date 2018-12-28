﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenTreeApp.API.Persistence.Migrations
{
    [DbContext(typeof(TreeDbContext))]
    [Migration("20181228182552_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<Guid?>("DetailsId");

                    b.HasKey("Id");

                    b.HasIndex("DetailsId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Details", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Sex");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Details");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<Guid?>("DetailsId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("DetailsId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DetailsId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DetailsId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DetailsId");

                    b.Property<Guid?>("TreeId");

                    b.HasKey("Id");

                    b.HasIndex("DetailsId")
                        .IsUnique();

                    b.HasIndex("TreeId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Relation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("PersonId");

                    b.Property<Guid?>("SecondPersonId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("SecondPersonId");

                    b.ToTable("Relations");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Tree", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Editable");

                    b.HasKey("Id");

                    b.ToTable("Trees");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AvatarId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.UserTree", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("TreeId");

                    b.HasKey("UserId", "TreeId");

                    b.HasIndex("TreeId");

                    b.ToTable("UserTrees");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Comment", b =>
                {
                    b.HasOne("GenTreeApp.Domain.Entities.Details")
                        .WithMany("Comments")
                        .HasForeignKey("DetailsId");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Event", b =>
                {
                    b.HasOne("GenTreeApp.Domain.Entities.Details")
                        .WithMany("Events")
                        .HasForeignKey("DetailsId");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Media", b =>
                {
                    b.HasOne("GenTreeApp.Domain.Entities.Details")
                        .WithMany("Media")
                        .HasForeignKey("DetailsId");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Person", b =>
                {
                    b.HasOne("GenTreeApp.Domain.Entities.Details", "Details")
                        .WithOne("Person")
                        .HasForeignKey("GenTreeApp.Domain.Entities.Person", "DetailsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GenTreeApp.Domain.Entities.Tree", "Tree")
                        .WithMany("Persons")
                        .HasForeignKey("TreeId");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.Relation", b =>
                {
                    b.HasOne("GenTreeApp.Domain.Entities.Person", "Person")
                        .WithMany("Relations1")
                        .HasForeignKey("PersonId");

                    b.HasOne("GenTreeApp.Domain.Entities.Person", "SecondPerson")
                        .WithMany("Relations2")
                        .HasForeignKey("SecondPersonId");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.User", b =>
                {
                    b.HasOne("GenTreeApp.Domain.Entities.Media", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");
                });

            modelBuilder.Entity("GenTreeApp.Domain.Entities.UserTree", b =>
                {
                    b.HasOne("GenTreeApp.Domain.Entities.Tree", "Tree")
                        .WithMany("UserTrees")
                        .HasForeignKey("TreeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GenTreeApp.Domain.Entities.User", "User")
                        .WithMany("UserTrees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
