using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW_Academy_RazorPages.Data;
using HW_Academy_RazorPages.Models;

namespace HW_Academy_RazorPages.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext _context;

        public DetailsModel(HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s=>s.Group).FirstOrDefaultAsync(m => m.stud_id == id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                Student = student;
            }
            return Page();
        }
    }
}
