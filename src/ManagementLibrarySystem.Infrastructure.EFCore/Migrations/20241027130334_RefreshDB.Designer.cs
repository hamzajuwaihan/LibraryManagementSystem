﻿// <auto-generated />
using System;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManagementLibrarySystem.Infrastructure.EFCore.Migrations
{
    [DbContext(typeof(DbAppContext))]
    [Migration("20241027130334_RefreshDB")]
    partial class RefreshDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookLibrary", b =>
                {
                    b.Property<Guid>("BooksId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LibrariesId")
                        .HasColumnType("uuid");

                    b.HasKey("BooksId", "LibrariesId");

                    b.HasIndex("LibrariesId");

                    b.ToTable("BookLibrary");
                });

            modelBuilder.Entity("LibraryMember", b =>
                {
                    b.Property<Guid>("LibrariesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.HasKey("LibrariesId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("LibraryMember");
                });

            modelBuilder.Entity("ManagementLibrarySystem.Domain.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("BorrowedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BorrowedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsBorrowed")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BorrowedBy");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ManagementLibrarySystem.Domain.Entities.Library", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("ManagementLibrarySystem.Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("BookLibrary", b =>
                {
                    b.HasOne("ManagementLibrarySystem.Domain.Entities.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementLibrarySystem.Domain.Entities.Library", null)
                        .WithMany()
                        .HasForeignKey("LibrariesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibraryMember", b =>
                {
                    b.HasOne("ManagementLibrarySystem.Domain.Entities.Library", null)
                        .WithMany()
                        .HasForeignKey("LibrariesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementLibrarySystem.Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManagementLibrarySystem.Domain.Entities.Book", b =>
                {
                    b.HasOne("ManagementLibrarySystem.Domain.Entities.Member", "Member")
                        .WithMany("Books")
                        .HasForeignKey("BorrowedBy")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Member");
                });

            modelBuilder.Entity("ManagementLibrarySystem.Domain.Entities.Member", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
