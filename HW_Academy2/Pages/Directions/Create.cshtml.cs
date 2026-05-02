using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HW_Academy2.Data;
using HW_Academy2.Models;

namespace HW_Academy2.Pages.Directions
{
    public class CreateModel : PageModel
    {
        private readonly HW_Academy2.Data.HW_Academy2Context _context;

        public CreateModel(HW_Academy2.Data.HW_Academy2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Direction Direction { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
				var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				return Page();
            }

            _context.Directions.Add(Direction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
