using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HW_Academy2.Data;
using HW_Academy2.Models;

namespace HW_Academy2.Pages.Groups
{
    public class IndexModel : PageModel
    {
        private readonly HW_Academy2.Data.HW_Academy2Context _context;

        public IndexModel(HW_Academy2.Data.HW_Academy2Context context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Group = await _context.Groups
                .Include(g => g.Direction).ToListAsync();
        }
    }
}
