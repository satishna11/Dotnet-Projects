using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;

namespace DAO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<CourseGroup> CourseGroup { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<CourseInfo> CourseInfo { get; set; }
        public DbSet<DashboardSlider> DashboardSlider { get; set; }
        public DbSet<NoticeInfo> NoticeInfo{ get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<Student> Student{ get; set; }
    }
}

