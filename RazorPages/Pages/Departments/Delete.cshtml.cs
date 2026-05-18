using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.Models;

namespace RazorPages.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPages.Data.ContosoUniversityContext _context;

        public DeleteModel(RazorPages.Data.ContosoUniversityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; } = default!;
        public string ConcurencyErrorMessage { get; set; }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == id);

        //    if (department == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        Department = department;
        //    }
        //    return Page();
        //}
        public async Task<IActionResult> OnGetAsync(int id, bool? concurencyError)
        {
            Department = await _context.Departments
                .Include(d=>d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(d=>d.DepartmentID == id);
            if(Department == null)return NotFound();

            if (concurencyError.GetValueOrDefault())ConcurencyErrorMessage = "Удаляемая запись была изменена другим пользователем удаление было прервано если вы все таки хотите удалиь эту запись нажмите удалить еще раз";
            
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var department = await _context.Departments.FindAsync(id);
        //    if (department != null)
        //    {
        //        Department = department;
        //        _context.Departments.Remove(Department);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToPage("./Index");
        //}
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (await _context.Departments.AnyAsync(d => d.DepartmentID == id))
                {
                    _context.Departments.Remove(Department);
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            { 

				return RedirectToPage("./Delete", new { concurencyError = true, id = id });

            }

        }
    }
}
