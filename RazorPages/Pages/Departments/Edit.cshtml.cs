using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.Models;

namespace RazorPages.Pages.Departments
{
	public class EditModel : PageModel
	{
		private readonly RazorPages.Data.ContosoUniversityContext _context;

		public EditModel(RazorPages.Data.ContosoUniversityContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Department Department { get; set; } = default!;
		public SelectList InstructorNameSL { get; set; }

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Console.WriteLine($"---=== {id} ===---");

			Department = await _context.Departments
			  .Include(d => d.Administrator)  // eager loading
			  .Include(d => d.Courses)
			  .AsNoTracking()                 // tracking not required
			  .FirstOrDefaultAsync(m => m.DepartmentID == id);

			if (Department == null)
			{
				return NotFound();
			}

			// Use strongly typed data rather than ViewData.
			InstructorNameSL = new SelectList(_context.Instructors,
				"ID", "FullName");

			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more information, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync(int id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var departmentToUpdate = await _context.Departments
				.Include(i => i.Administrator)
				.Include(i => i.Courses)
				.FirstOrDefaultAsync(m => m.DepartmentID == id);

			if (departmentToUpdate == null)
			{
				return HandleDeletedDepartment();
			}

			// Set ConcurrencyToken to value read in OnGetAsync
			_context.Entry(departmentToUpdate).Property(
				 d => d.ConcurerrencyToken).OriginalValue = Department.ConcurerrencyToken;

			if (await TryUpdateModelAsync<Department>(
				departmentToUpdate,
				"Department",
				s => s.Name, s => s.StartDate, s => s.Budget, s => s.InstructorID))
			{
				try
				{
					await _context.SaveChangesAsync();
					return RedirectToPage("./Index");
				}
				catch (DbUpdateConcurrencyException ex)
				{
					var exceptionEntry = ex.Entries.Single();
					var clientValues = (Department)exceptionEntry.Entity;
					var databaseEntry = exceptionEntry.GetDatabaseValues();
					if (databaseEntry == null)
					{
						ModelState.AddModelError(string.Empty, "Unable to save. " +
							"The department was deleted by another user.");
						return Page();
					}

					var dbValues = (Department)databaseEntry.ToObject();
					await SetDbErrorMessage(dbValues, clientValues, _context);

					// Save the current ConcurrencyToken so next postback
					// matches unless an new concurrency issue happens.
					Department.ConcurerrencyToken = dbValues.ConcurerrencyToken;
					// Clear the model error for the next postback.
					ModelState.Remove($"{nameof(Department)}.{nameof(Department.ConcurerrencyToken)}");
				}
			}

			InstructorNameSL = new SelectList(_context.Instructors,
				"ID", "FullName", departmentToUpdate.InstructorID);

			return Page();
		}

		private bool DepartmentExists(int id)
		{
			return _context.Departments.Any(e => e.DepartmentID == id);
		}

		private IActionResult HandleDeletedDepartment()
		{
			// ModelState contains the posted data because of the deletion error
			// and overides the Department instance values when displaying Page().
			ModelState.AddModelError(string.Empty,
				"Unable to save. The department was deleted by another user.");
			InstructorNameSL = new SelectList(_context.Instructors, "ID", "FullName", Department.InstructorID);
			return Page();
		}

		private async Task SetDbErrorMessage(Department dbValues,
				Department clientValues, ContosoUniversityContext context)
		{

			if (dbValues.Name != clientValues.Name)
			{
				ModelState.AddModelError("Department.Name",
					$"Current value: {dbValues.Name}");
			}
			if (dbValues.Budget != clientValues.Budget)
			{
				ModelState.AddModelError("Department.Budget",
					$"Current value: {dbValues.Budget:c}");
			}
			if (dbValues.StartDate != clientValues.StartDate)
			{
				ModelState.AddModelError("Department.StartDate",
					$"Current value: {dbValues.StartDate:d}");
			}
			if (dbValues.InstructorID != clientValues.InstructorID)
			{
				Instructor dbInstructor = await _context.Instructors
				   .FindAsync(dbValues.InstructorID);
				ModelState.AddModelError("Department.InstructorID",
					$"Current value: {dbInstructor?.FullName}");
			}

			ModelState.AddModelError(string.Empty,
				"The record you attempted to edit "
			  + "was modified by another user after you. The "
			  + "edit operation was canceled and the current values in the database "
			  + "have been displayed. If you still want to edit this record, click "
			  + "the Save button again.");
		}
	}
}

