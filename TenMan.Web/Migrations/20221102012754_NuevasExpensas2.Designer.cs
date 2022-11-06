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
    [Migration("20221102012754_NuevasExpensas2")]
    partial class NuevasExpensas2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CUIT")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("RegisterNumber")
                        .HasMaxLength(50);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.CheckingAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance");

                    b.Property<string>("Number")
                        .IsRequired();

                    b.Property<decimal>("PreviousBalance");

                    b.HasKey("Id");

                    b.ToTable("CheckingAccounts");
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

                    b.Property<string>("CUIT")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.Property<string>("SuterhKey")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.ToTable("Committees");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Cost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("FieldId");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<string>("PdfFile");

                    b.Property<string>("Status");

                    b.Property<int?>("TenantId");

                    b.Property<int?>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex("UnitId");

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

                    b.Property<string>("ActualStatus");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Remarks");

                    b.Property<int?>("SpecialityId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("UnitId");

                    b.Property<int?>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("UnitId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.RequestImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

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

                    b.ToTable("StatusTypes");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.SuperAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SuperAdmins");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apartment")
                        .IsRequired();

                    b.Property<int?>("CheckingAccountId");

                    b.Property<int?>("CommitteeId");

                    b.Property<int>("Floor");

                    b.Property<int>("Number");

                    b.Property<string>("Owner");

                    b.Property<double>("Percentage");

                    b.Property<int>("SquareMeters");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("CheckingAccountId");

                    b.HasIndex("CommitteeId");

                    b.HasIndex("TenantId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RegisterNumber")
                        .HasMaxLength(50);

                    b.Property<int?>("SpecialtyId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.HasIndex("UserId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Administrator", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Committee", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Administrator", "Administrator")
                        .WithMany("Committees")
                        .HasForeignKey("AdministratorId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Cost", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Payment", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Tenant", "Tenant")
                        .WithMany("Payments")
                        .HasForeignKey("TenantId");

                    b.HasOne("TenMan.Web.Data.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Request", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.Specialty", "Speciality")
                        .WithMany("Requests")
                        .HasForeignKey("SpecialityId");

                    b.HasOne("TenMan.Web.Data.Entities.Unit", "Unit")
                        .WithMany("Requests")
                        .HasForeignKey("UnitId");

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

            modelBuilder.Entity("TenMan.Web.Data.Entities.SuperAdmin", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Tenant", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TenMan.Web.Data.Entities.Unit", b =>
                {
                    b.HasOne("TenMan.Web.Data.Entities.CheckingAccount", "CheckingAccount")
                        .WithMany()
                        .HasForeignKey("CheckingAccountId");

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

                    b.HasOne("TenMan.Web.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
