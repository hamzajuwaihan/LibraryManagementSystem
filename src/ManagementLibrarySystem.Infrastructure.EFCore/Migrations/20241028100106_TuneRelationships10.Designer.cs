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
    [Migration("20241028100106_TuneRelationships10")]
    partial class TuneRelationships10
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LibraryMember", b =>
                {
                    b.Property<Guid>("LibrariesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.HasKey("LibrariesId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("LibraryMembers", (string)null);
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

                    b.Property<Guid>("LibraryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BorrowedBy");

                    b.HasIndex("LibraryId");

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
                    b.HasOne("ManagementLibrarySystem.Domain.Entities.Member", null)
                        .WithMany("Books")
                        .HasForeignKey("BorrowedBy")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ManagementLibrarySystem.Domain.Entities.Library", "Library")
                        .WithMany("Books")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Library");
                });

            modelBuilder.Entity("ManagementLibrarySystem.Domain.Entities.Library", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ManagementLibrarySystem.Domain.Entities.Member", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
