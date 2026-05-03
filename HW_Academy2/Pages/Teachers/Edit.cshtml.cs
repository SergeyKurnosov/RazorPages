using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW_Academy2.Data;
using HW_Academy2.Models;

namespace HW_Academy2.Pages.Teachers
{
    public class EditModel : PageModel
    {
        private readonly HW_Academy2.Data.HW_Academy2Context _context;

        public EditModel(HW_Academy2.Data.HW_Academy2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Teacher Teacher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher =  await _context.Teachers.FirstOrDefaultAsync(m => m.teacher_id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            Teacher = teacher;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

			if (Teacher.photoFile != null && Teacher.photoFile.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					await Teacher.photoFile.CopyToAsync(memoryStream);
					Teacher.photo = memoryStream.ToArray();
				}

			}

			_context.Attach(Teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(Teacher.teacher_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.teacher_id == id);
        }
    }
}
