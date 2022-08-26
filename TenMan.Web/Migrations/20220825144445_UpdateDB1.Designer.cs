﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TenMan.Web.Data;

namespace TenMan.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220825144445_UpdateDB1")]
    partial class UpdateDB1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TenMan.Web.Data.Entities.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FixedPhone")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("RegisterNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Committee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("AdministratorId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.ToTable("Committees");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FixedPhone")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ReceiptId");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.HasIndex("TenantId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileUrl")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("SpecialityId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("TenantId");

                    b.Property<int?>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("TenantId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.RequestImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .IsRequired();

                    b.Property<int?>("RequestId");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestImages");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Specialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("RequestId");

                    b.Property<int?>("StatusTypeId");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.HasIndex("StatusTypeId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.StatusType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("StatusType");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FixedPhone")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apartment")
                        .IsRequired();

                    b.Property<int?>("CommitteeId");

                    b.Property<int>("Floor");

                    b.Property<int>("Number");

                    b.Property<int>("SquareMeters");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("CommitteeId");

                    b.HasIndex("TenantId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FixedPhone")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("RegisterNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("SpecialtyId");

                    b.HasKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Committee", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Administrator", "Administrator")
                        .WithMany("Committees")
                        .HasForeignKey("AdministratorId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Payment", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Receipt", "Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptId");

                    b.HasOne("TenMan.Web.Data.Entities.Tenant", "Tenant")
                        .WithMany("Payments")
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Request", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Specialty", "Speciality")
                        .WithMany("Requests")
                        .HasForeignKey("SpecialityId");

                    b.HasOne("TenMan.Web.Data.Entities.Tenant", "Tenant")
                        .WithMany("Requests")
                        .HasForeignKey("TenantId");

                    b.HasOne("TenMan.Web.Data.Entities.Worker", "Worker")
                        .WithMany("Requests")
                        .HasForeignKey("WorkerId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.RequestImage", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Request", "Request")
                        .WithMany("Images")
                        .HasForeignKey("RequestId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Status", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Request", "Request")
                        .WithMany("Statuses")
                        .HasForeignKey("RequestId");

                    b.HasOne("TenMan.Web.Data.Entities.StatusType", "StatusType")
                        .WithMany()
                        .HasForeignKey("StatusTypeId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Unit", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Committee", "Committee")
                        .WithMany("Units")
                        .HasForeignKey("CommitteeId");

                    b.HasOne("TenMan.Web.Data.Entities.Tenant", "Tenant")
                        .WithMany("Units")
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Worker", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Specialty", "Specialty")
                        .WithMany("Workers")
                        .HasForeignKey("SpecialtyId");
                });
#pragma warning restore 612, 618
        }
    }
}
