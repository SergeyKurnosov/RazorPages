using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HW_Academy_Razor_Pages.Model;

namespace HW_Academy_Razor_Pages.Data
{
    public class HW_Academy_Razor_PagesContext : DbContext
    {
        public HW_Academy_Razor_PagesContext (DbContextOptions<HW_Academy_Razor_PagesContext> options)
            : base(options)
        {
        }

        public DbSet<HW_Academy_Razor_Pages.Model.Direction> Direction { get; set; } = default!;
    }
}
