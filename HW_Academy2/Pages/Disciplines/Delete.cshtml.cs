using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW_Academy2.Data;
using HW_Academy2.Models;

namespace HW_Academy2.Pages.Disciplines
{
    public class DeleteModel : PageModel
    {
        private readonly HW_Academy2.Data.HW_Academy2Context _context;

        public DeleteModel(HW_Academy2.Data.HW_Academy2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Discipline Discipline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines.FirstOrDefaultAsync(m => m.discipline_id == id);

            if (discipline == null)
            {
                return NotFound();
            }
            else
            {
                Discipline = discipline;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline != null)
            {
                Discipline = discipline;
                _context.Disciplines.Remove(Discipline);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
