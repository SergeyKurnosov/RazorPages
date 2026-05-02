using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HW_Academy2.Models;

namespace HW_Academy2.Data
{
    public class HW_Academy2Context : DbContext
    {
        public HW_Academy2Context (DbContextOptions<HW_Academy2Context> options)
            : base(options)
        {
        }

        public DbSet<HW_Academy2.Models.Direction> Directions { get; set; } = default!;
        public DbSet<HW_Academy2.Models.Group> Groups { get; set; } = default!;
        public DbSet<HW_Academy2.Models.Student> Students { get; set; } = default!;
    }
}
