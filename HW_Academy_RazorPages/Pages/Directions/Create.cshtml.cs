using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HW_Academy_RazorPages.Data;
using HW_Academy_RazorPages.Models;


namespace HW_Academy_RazorPages.Pages.Directions
{
    public class CreateModel : PageModel
    {
        private readonly HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext _context;

        public CreateModel(HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext context)
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
            //if (!ModelState.IsValid)//////////////////////////////////////////////////////////////////////////////////////////////////////
            //{
            //    return Page();
            //}

            _context.Directions.Add(Direction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
