using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW_Academy_RazorPages.Data;
using HW_Academy_RazorPages.Models;

namespace HW_Academy_RazorPages.Pages.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext _context;

        public DetailsModel(HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext context)
        {
            _context = context;
        }

        public Group Group { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FirstOrDefaultAsync(m => m.group_id == id);
            if (group == null)
            {
                return NotFound();
            }
            else
            {
                Group = group;
            }
            return Page();
        }
    }
}
