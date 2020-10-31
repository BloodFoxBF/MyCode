using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasMany(x => x.Groups)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId);
            modelBuilder.Entity<Group>()
                .HasMany(x => x.Students)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);
            modelBuilder.Entity<AssignedProblem>()
                .HasOne<Problem>(x => x.Problem)
                .WithMany(x => x.AssignedProblems)
                .HasForeignKey(x => x.ProblemId);
            modelBuilder.Entity<AssignedProblem>()
                .HasOne<Student>(x => x.Student)
                .WithMany(x => x.AssignedProblems)
                .HasForeignKey(x => x.StudentId);

        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<AssignedProblem> AssignedTasks { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
