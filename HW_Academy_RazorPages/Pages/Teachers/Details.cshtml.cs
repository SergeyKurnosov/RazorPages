using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW_Academy_RazorPages.Data;
using HW_Academy_RazorPages.Models;

namespace HW_Academy_RazorPages.Pages.Teachers
{
    public class DetailsModel : PageModel
    {
        private readonly HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext _context;

        public DetailsModel(HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext context)
        {
            _context = context;
        }

        public Teacher Teacher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.teacher_id == id);

            var teacher = await _context
                .Teachers
                .Include(t => t.DisciplinesRelations)
                .ThenInclude(tr => tr.Discipline)
                .FirstOrDefaultAsync(m => m.teacher_id == id);



            if (teacher == null)
            {
                return NotFound();
            }
            else
            {
                Teacher = teacher;
            }
            return Page();
        }
    }
}
