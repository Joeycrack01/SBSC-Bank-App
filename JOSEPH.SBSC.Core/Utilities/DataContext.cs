using JOSEPH.SBSC.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Utilities
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) 
        { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamsQuestion> ExamsQuestions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EmployeeCourse> EmployeeCourses { get; set; }
        public DbSet<EmployeeGrade> EmployeeGrades { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserCertificateRecord> UserCertificateRecords { get; set; }
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<Course> Courses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Exam>().ToTable(nameof(Exam));
            modelBuilder.Entity<ExamsQuestion>().ToTable(nameof(ExamsQuestion));
            modelBuilder.Entity<Course>().ToTable(nameof(Course));
            modelBuilder.Entity<EmployeeCourse>().ToTable(nameof(EmployeeCourse));
            modelBuilder.Entity<EmployeeGrade>().ToTable(nameof(EmployeeGrade));
            modelBuilder.Entity<Notification>().ToTable(nameof(Notification));
            modelBuilder.Entity<UserCertificateRecord>().ToTable(nameof(UserCertificateRecord));

            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(p => new { p.Id });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.RoleId });
            modelBuilder.Entity<IdentityRole<string>>().HasKey(p => new { p.Id });
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.UserId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId });

        }
    }
}
