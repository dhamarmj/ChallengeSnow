﻿// <auto-generated />
using System;
using ChallengeSnow.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChallengeSnow.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220420160545_inheritanceStructure")]
    partial class inheritanceStructure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("ChallengeSnow.Models.ItemBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Available_Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AllItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ItemBase");
                });

            modelBuilder.Entity("ChallengeSnow.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date_Created")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Item_NumberId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Item_NumberId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ChallengeSnow.Models.Deal_Item", b =>
                {
                    b.HasBaseType("ChallengeSnow.Models.ItemBase");

                    b.Property<decimal>("Discount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Deal_Item");
                });

            modelBuilder.Entity("ChallengeSnow.Models.Item", b =>
                {
                    b.HasBaseType("ChallengeSnow.Models.ItemBase");

                    b.HasDiscriminator().HasValue("Item");
                });

            modelBuilder.Entity("ChallengeSnow.Models.Order", b =>
                {
                    b.HasOne("ChallengeSnow.Models.ItemBase", "Item_Number")
                        .WithMany("Orders")
                        .HasForeignKey("Item_NumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item_Number");
                });

            modelBuilder.Entity("ChallengeSnow.Models.ItemBase", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
