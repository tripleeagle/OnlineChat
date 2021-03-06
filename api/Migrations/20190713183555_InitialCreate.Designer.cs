﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineChat.Repository;

namespace OnlineChat.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20190713183555_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("OnlineChat.Models.Chat", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Name");

                    b.ToTable("Chats");

                    b.HasData(
                        new
                        {
                            Name = "Developers"
                        },
                        new
                        {
                            Name = "Designers"
                        });
                });

            modelBuilder.Entity("OnlineChat.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CTime");

                    b.Property<string>("ChatName");

                    b.Property<string>("Text");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChatName");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CTime = new DateTime(2019, 7, 13, 20, 35, 55, 13, DateTimeKind.Local).AddTicks(4860),
                            ChatName = "Developers",
                            Text = "Hey there",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CTime = new DateTime(2019, 7, 13, 20, 35, 55, 19, DateTimeKind.Local).AddTicks(990),
                            ChatName = "Developers",
                            Text = "What's up",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("OnlineChat.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChatName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ChatName");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChatName = "Developers",
                            Name = "Valera"
                        },
                        new
                        {
                            Id = 2,
                            ChatName = "Developers",
                            Name = "Max"
                        },
                        new
                        {
                            Id = 3,
                            ChatName = "Developers",
                            Name = "Tom"
                        });
                });

            modelBuilder.Entity("OnlineChat.Models.Message", b =>
                {
                    b.HasOne("OnlineChat.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatName");

                    b.HasOne("OnlineChat.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineChat.Models.User", b =>
                {
                    b.HasOne("OnlineChat.Models.Chat", "Chat")
                        .WithMany("Users")
                        .HasForeignKey("ChatName");
                });
#pragma warning restore 612, 618
        }
    }
}
