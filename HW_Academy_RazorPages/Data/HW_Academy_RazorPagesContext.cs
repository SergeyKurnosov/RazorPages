using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HW_Academy_RazorPages.Models;

namespace HW_Academy_RazorPages.Data
{
    public class HW_Academy_RazorPagesContext : DbContext
    {
        public HW_Academy_RazorPagesContext (DbContextOptions<HW_Academy_RazorPagesContext> options)
            : base(options)
        {
        }

        public DbSet<HW_Academy_RazorPages.Models.Direction> Directions { get; set; } = default!;
        public DbSet<HW_Academy_RazorPages.Models.Discipline> Disciplines { get; set; } = default!;
        public DbSet<HW_Academy_RazorPages.Models.Group> Groups { get; set; } = default!;
        public DbSet<HW_Academy_RazorPages.Models.Student> Students { get; set; } = default!;
        public DbSet<HW_Academy_RazorPages.Models.Teacher> Teachers { get; set; } = default!;
		public DbSet<HW_Academy_RazorPages.Models.TeacherDisciplineRelation> TeachersDisciplinesRelation { get; set; } = default!;
	}
}
