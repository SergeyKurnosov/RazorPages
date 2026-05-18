using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
			Department = await _context.Departments
				.Include(d => d.Administrator)
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.DepartmentID == id);

			if (Department == null)
			{
				return NotFound();
			}

			//var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == id);
			//if (department == null)
			//{
			//	return NotFound();
			//}
			//Department = Department;
			InstructorNameSL = new SelectList(_context.Instructors, "ID", "FirstName");
			//ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FirstName");
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more information, see https://aka.ms/RazorPagesCRUD.
		//public async Task<IActionResult> OnPostAsync()
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return Page();
		//	}

		//	_context.Attach(Department).State = EntityState.Modified;

		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!DepartmentExists(Department.DepartmentID))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return RedirectToPage("./Index");
		//}

		public async Task<IActionResult> OnPostAsync(int id)
		{
			if (!ModelState.IsValid) return Page();

			Department department = await _context.Departments
				.Include(d => d.Administrator)
				.FirstOrDefaultAsync(m => m.DepartmentID == id);

			if (department == null) return HandleDeletedDepartment();
			_context.Entry(department)
				.Property(d => d.ConcurerrencyToken)
				.OriginalValue = this.Department.ConcurerrencyToken;

			bool success = await TryUpdateModelAsync<Department>(
				department,
				"Department",
				d => d.Name, d => d.StartDate, d => d.Budget, d => d.InstructorID
				);

			if (success)
			{
				try
				{
					await _context.SaveChangesAsync();
					return RedirectToPage("./Index");
				}
				catch (DbUpdateConcurrencyException ex)
				{
					EntityEntry exceptionEntry = ex.Entries.Single();
					Department clientValues = exceptionEntry.Entity as Department;
					PropertyValues databaseEntry = exceptionEntry.GetDatabaseValues();
					if (databaseEntry != null)
					{
						ModelState.AddModelError(string.Empty, "Невозможно сохранить, Департамент был удален");
						return Page();
					}
					Department dbValues = databaseEntry.ToObject() as Department;
					await SetDbErrorMessage(dbValues, clientValues, _context);
					this.Department.ConcurerrencyToken = dbValues.ConcurerrencyToken;
					ModelState.Remove($"{nameof(Department)}.{nameof(Department.ConcurerrencyToken)}");
				}
			}
			InstructorNameSL = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);

			return Page();
		}

		IActionResult HandleDeletedDepartment()
		{
			ModelState.AddModelError(
				string.Empty,
				"HandleDeletedDepartment: Департмент был удален другим пользователем"
				);

			InstructorNameSL = new SelectList(_context.Instructors, "ID", "FullName", Department.InstructorID);
			return Page();
		}

		async Task SetDbErrorMessage(Department dbValues, Department clientValues, ContosoUniversityContext context)
		{
			if (dbValues.Name != clientValues.Name) ModelState.AddModelError("Department.Name", $"CurrentValue:{dbValues.Name}");
			if (dbValues.Budget != clientValues.Budget) ModelState.AddModelError("Department.Budget", $"CurrentValue:{dbValues.Budget:c}");
			if (dbValues.StartDate != clientValues.StartDate) ModelState.AddModelError("Department.StartDate", $"CurrentValue:{dbValues.StartDate:d}");
			if (dbValues.InstructorID != clientValues.InstructorID)
			{
				Instructor dbInstructor = await _context
					.Instructors
					.FindAsync(dbValues.InstructorID);
			ModelState.AddModelError("Department.InstructorID", $"CurrentValue:{dbInstructor.FullName}");
			}
			ModelState.AddModelError(string.Empty, "Error:Запись которую вы пытаетесь редактировать была изменена другим пользователем . операция была отклонена если вы все еще хотите сохранить изменения нажмите на сохранить ");


		}



		private bool DepartmentExists(int id)
		{
			return _context.Departments.Any(e => e.DepartmentID == id);
		}
	}
}
