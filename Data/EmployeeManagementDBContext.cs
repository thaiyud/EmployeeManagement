using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class EmployeeManagementDBContext : IdentityDbContext<ApplicationUser>
    {
        public EmployeeManagementDBContext()
        {
        }
        public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options)
       : base(options)
        {
        }
        public DbSet<BasicSalary> BasicSalaries { get; set; }
        public DbSet<MonthlySalary> MonthlySalaries { get; set; }

        public DbSet<Form> Forms { get; set; }
        public DbSet<FormType> FormTypes { get; set; }
        public DbSet<FileAttachment> FileAttachments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships
            modelBuilder.Entity<Form>()
              .HasOne(f => f.User)
              .WithMany(u => u.Forms)
              .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.Cascade);
         

            modelBuilder.Entity<MonthlySalary>()
                .HasOne(ms => ms.BasicSalary)
                .WithMany(bs => bs.MonthlySalaries)
                .HasForeignKey(ms => ms.BasicId);


            modelBuilder.Entity<Form>()
                .HasOne(f => f.FormType)
                .WithMany(ft => ft.Forms)
                .HasForeignKey(f => f.FormTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Form>()
                .HasOne(f => f.FileAttachment)
                .WithOne(a => a.Form)
                .HasForeignKey<FileAttachment>(a => a.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Form>()
      .HasOne(f => f.FormType)
      .WithMany(ft => ft.Forms)
      .HasForeignKey(f => f.FormTypeId)
      .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
