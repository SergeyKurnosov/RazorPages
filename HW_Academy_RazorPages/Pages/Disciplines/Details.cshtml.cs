using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW_Academy_RazorPages.Data;
using HW_Academy_RazorPages.Models;

namespace HW_Academy_RazorPages.Pages.Disciplines
{
	public class DetailsModel : PageModel
	{
		private readonly HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext _context;

		public DetailsModel(HW_Academy_RazorPages.Data.HW_Academy_RazorPagesContext context)
		{
			_context = context;
		}

		public Discipline Discipline { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var discipline = await _context.Disciplines.Include(d => d.TeachersRelations)
		.ThenInclude(tr => tr.Teacher).FirstOrDefaultAsync(m => m.discipline_id == id);
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
	}
}
