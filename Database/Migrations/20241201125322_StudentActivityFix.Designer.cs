﻿// <auto-generated />
using System;
using CrmBackend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CrmBackend.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241201125322_StudentActivityFix")]
    partial class StudentActivityFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActivityPhotos", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<int>("PhotoId")
                        .HasColumnType("integer");

                    b.HasKey("ActivityId", "PhotoId");

                    b.HasIndex("PhotoId");

                    b.ToTable("ActivityPhotos");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("TelegramLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrgChatLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PreviewPhotoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("PreviewPhotoId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.ActivityTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("MaxScore")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivityTests");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Competence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("Competences");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CryptedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.StudentActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int?>("StudentTestResultId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("StudentTestResultId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentActivities");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.StudentTestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityTestId")
                        .HasColumnType("integer");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.Property<int>("StudentActivityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTestId");

                    b.HasIndex("StudentActivityId");

                    b.ToTable("StudentTestResults");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("Roles")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ActivityPhotos", b =>
                {
                    b.HasOne("CrmBackend.Database.Models.Activity", null)
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrmBackend.Database.Models.Photo", null)
                        .WithMany()
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Activity", b =>
                {
                    b.HasOne("CrmBackend.Database.Models.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrmBackend.Database.Models.Photo", "PreviewPhoto")
                        .WithMany()
                        .HasForeignKey("PreviewPhotoId");

                    b.Navigation("CreatorUser");

                    b.Navigation("PreviewPhoto");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.ActivityTest", b =>
                {
                    b.HasOne("CrmBackend.Database.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Competence", b =>
                {
                    b.HasOne("CrmBackend.Database.Models.Activity", null)
                        .WithMany("Competences")
                        .HasForeignKey("ActivityId");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.StudentActivity", b =>
                {
                    b.HasOne("CrmBackend.Database.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrmBackend.Database.Models.StudentTestResult", "StudentTestResult")
                        .WithMany()
                        .HasForeignKey("StudentTestResultId");

                    b.HasOne("CrmBackend.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("StudentTestResult");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.StudentTestResult", b =>
                {
                    b.HasOne("CrmBackend.Database.Models.ActivityTest", "ActivityTest")
                        .WithMany()
                        .HasForeignKey("ActivityTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrmBackend.Database.Models.StudentActivity", "StudentActivity")
                        .WithMany()
                        .HasForeignKey("StudentActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityTest");

                    b.Navigation("StudentActivity");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.User", b =>
                {
                    b.HasOne("CrmBackend.Database.Models.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("CrmBackend.Database.Models.User", "AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Account", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("CrmBackend.Database.Models.Activity", b =>
                {
                    b.Navigation("Competences");
                });
#pragma warning restore 612, 618
        }
    }
}