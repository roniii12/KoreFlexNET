using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class MoodyDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public class OptionsBuild
        {
            public DbContextOptionsBuilder<MoodyDbContext> opsBuilder { get; set; }
            public DbContextOptions<MoodyDbContext> dbOptions { get; set; }
            private AppConfiguration settings { get; set; }
            public OptionsBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<MoodyDbContext>();
                opsBuilder.UseSqlServer(settings.sqlConnectionString);
                dbOptions = opsBuilder.Options;
            }
        }
        public static OptionsBuild ops = new OptionsBuild();
        public MoodyDbContext(DbContextOptions<MoodyDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meeting>()
                .HasOne<Patient>(pm => pm.Patient)
                .WithMany(p => p.Meetings)
                .HasForeignKey(pm => pm.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meeting>()
                .HasOne<Therapist>(pm => pm.Therapist)
                .WithMany(t => t.Meetings)
                .HasForeignKey(pm => pm.TherapistId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meeting>()
                .HasOne<Room>(pm => pm.Room)
                .WithMany(r => r.Meetings)
                .HasForeignKey(pm => pm.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Therapist>()
                .HasOne<User>(t => t.User)
                .WithOne(u => u.Therapist)
                .HasForeignKey<Therapist>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasOne<User>(p => p.User)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<UnhandledCancelMeeting>()
            //    .HasOne<Patient>(ucm => ucm.Patient)
            //    .WithMany(p => p.unhandledCancelMeetings)
            //    .HasForeignKey(ucm => ucm.PatientId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<UnhandledCancelMeeting>()
            //    .HasOne<Therapist>(ucm => ucm.Therapist)
            //    .WithMany(t => t.unhandledCancelMeetings)
            //    .HasForeignKey(ucm => ucm.TherapistId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<UnhandledCancelMeeting>()
            //    .HasOne<Room>(ucm => ucm.Room)
            //    .WithMany(r => r.unhandledCancelMeetings)
            //    .HasForeignKey(ucm => ucm.RoomId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TherapistHoursWorking>()
            //    .HasOne<Therapist>(thw => thw.Therapist)
            //    .WithMany(t => t.TherapistHoursWorkings)
            //    .HasForeignKey(thw => thw.TherapistId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.Id);

            modelBuilder.Entity<Meeting>()
                .HasIndex(pm => new { pm.TherapistId, pm.PatientId, pm.startDate })
                .IsUnique();

            modelBuilder.Entity<Therapist>()
                .HasIndex(t => t.Id);
            //modelBuilder.Entity<TherapistHoursWorking>()
            //    .HasIndex(thw => thw.TherapistId);
        }
    }
}
