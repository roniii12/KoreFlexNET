﻿// <auto-generated />
using System;
using KoreFlex.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KoreFlex.Migrations
{
    [DbContext(typeof(KoreFlexDb))]
    [Migration("20210302161016_addUsername")]
    partial class addUsername
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KoreFlex.Data.Patient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("KoreFlex.Data.PatientMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeetLenghtMin")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TherapistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("RoomId");

                    b.HasIndex("TherapistId", "PatientId")
                        .IsUnique();

                    b.ToTable("PatientMeetings");
                });

            modelBuilder.Entity("KoreFlex.Data.Room", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("KoreFlex.Data.Therapist", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Therapists");
                });

            modelBuilder.Entity("KoreFlex.Data.TherapistHoursWorking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LengthInMin")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TherapistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TherapistId");

                    b.ToTable("TherapistHoursWorking");
                });

            modelBuilder.Entity("KoreFlex.Data.UnhandledCancelMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("LengthMeetInMin")
                        .HasColumnType("int");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TherapistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("RoomId");

                    b.HasIndex("TherapistId");

                    b.ToTable("unhandledCancelMeetings");
                });

            modelBuilder.Entity("KoreFlex.Data.PatientMeeting", b =>
                {
                    b.HasOne("KoreFlex.Data.Patient", "Patient")
                        .WithMany("PatientMeetings")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KoreFlex.Data.Room", "Room")
                        .WithMany("patientMeetings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KoreFlex.Data.Therapist", "Therapist")
                        .WithMany("PatientMeetings")
                        .HasForeignKey("TherapistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Room");

                    b.Navigation("Therapist");
                });

            modelBuilder.Entity("KoreFlex.Data.TherapistHoursWorking", b =>
                {
                    b.HasOne("KoreFlex.Data.Therapist", "Therapist")
                        .WithMany("TherapistHoursWorkings")
                        .HasForeignKey("TherapistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Therapist");
                });

            modelBuilder.Entity("KoreFlex.Data.UnhandledCancelMeeting", b =>
                {
                    b.HasOne("KoreFlex.Data.Patient", "Patient")
                        .WithMany("unhandledCancelMeetings")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KoreFlex.Data.Room", "Room")
                        .WithMany("unhandledCancelMeetings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KoreFlex.Data.Therapist", "Therapist")
                        .WithMany("unhandledCancelMeetings")
                        .HasForeignKey("TherapistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Room");

                    b.Navigation("Therapist");
                });

            modelBuilder.Entity("KoreFlex.Data.Patient", b =>
                {
                    b.Navigation("PatientMeetings");

                    b.Navigation("unhandledCancelMeetings");
                });

            modelBuilder.Entity("KoreFlex.Data.Room", b =>
                {
                    b.Navigation("patientMeetings");

                    b.Navigation("unhandledCancelMeetings");
                });

            modelBuilder.Entity("KoreFlex.Data.Therapist", b =>
                {
                    b.Navigation("PatientMeetings");

                    b.Navigation("TherapistHoursWorkings");

                    b.Navigation("unhandledCancelMeetings");
                });
#pragma warning restore 612, 618
        }
    }
}
