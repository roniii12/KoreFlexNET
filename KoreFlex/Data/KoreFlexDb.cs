using KoreFlex.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoreFlex.Data
{
    public class KoreFlexDb : DbContext
    {
        public KoreFlexDb(DbContextOptions<KoreFlexDb> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMeeting> PatientMeetings { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<TherapistHoursWorking> TherapistHoursWorkings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UnhandledCancelMeeting> unhandledCancelMeetings { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientMeeting>()
                .HasOne<Patient>(pm => pm.Patient)
                .WithMany(p => p.PatientMeetings)
                .HasForeignKey(pm => pm.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientMeeting>()
                .HasOne<Therapist>(pm => pm.Therapist)
                .WithMany(t => t.PatientMeetings)
                .HasForeignKey(pm => pm.TherapistId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientMeeting>()
                .HasOne<Room>(pm => pm.Room)
                .WithMany(r => r.patientMeetings)
                .HasForeignKey(pm => pm.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UnhandledCancelMeeting>()
                .HasOne<Patient>(ucm => ucm.Patient)
                .WithMany(p => p.unhandledCancelMeetings)
                .HasForeignKey(ucm => ucm.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UnhandledCancelMeeting>()
                .HasOne<Therapist>(ucm => ucm.Therapist)
                .WithMany(t => t.unhandledCancelMeetings)
                .HasForeignKey(ucm => ucm.TherapistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UnhandledCancelMeeting>()
                .HasOne<Room>(ucm => ucm.Room)
                .WithMany(r => r.unhandledCancelMeetings)
                .HasForeignKey(ucm => ucm.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TherapistHoursWorking>()
                .HasOne<Therapist>(thw => thw.Therapist)
                .WithMany(t => t.TherapistHoursWorkings)
                .HasForeignKey(thw => thw.TherapistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.Id);

            modelBuilder.Entity<PatientMeeting>()
                .HasIndex(pm => new { pm.TherapistId, pm.PatientId })
                .IsUnique();

            modelBuilder.Entity<Therapist>()
                .HasIndex(t => t.Id);
            modelBuilder.Entity<TherapistHoursWorking>()
                .HasIndex(thw => thw.TherapistId);
        }
    }
}
