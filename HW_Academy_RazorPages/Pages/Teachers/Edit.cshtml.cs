using HW_Academy_RazorPages.Data;
using HW_Academy_RazorPages.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_Academy_RazorPages.Pages.Teachers
{
    public class EditModel : PageModel
    {
		public Human Human { get; set; }
        public string path { get; set; }
        public byte [] data { get; set; }

		private readonly HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext _context;

        public EditModel(HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Teacher Teacher { get; set; } = default!;

		[BindProperty]
		public IFormFile UploadedFile { get; set; }

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

            _context.Attach(Teacher).State = EntityState.Modified;

            try
            {
				//if (UploadedFile != null)
				//{
				//	using (var stream = UploadedFile.OpenReadStream())
				//	{
				//		byte[] bytes = new byte[UploadedFile.Length];
				//		stream.Read(bytes, 0, bytes.Length);
    //                    Teacher.photo = bytes;
				//	}
				//}
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
