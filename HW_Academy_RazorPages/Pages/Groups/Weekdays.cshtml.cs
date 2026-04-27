//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using HW_Academy_RazorPages.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace HW_Academy_RazorPages.Pages.Groups
//{
//    public class WeekdaysModel : PageModel
//    {
//	public readonly IEnumerable<SelectListItem> DAYNAMES = new List<SelectListItem> { 
//	new SelectListItem{ Value="1", Text }	"Ïí", 
//		"Âò", 
//		"Ñð", 
//		"×ò", 
//		"Ïò", 
//		"Ñá", 
//		"Âñ" 
//	};
//	public	bool[] days = new bool[7];

//	//	[Parameter] public Group Group { get; set; }

//		[BindProperty]
//		public Group Group { get; set; } = default!;

//		int mask;

//		void Compress()
//		{
//			mask = 0;
//			for (byte i = 0; i < 7; i++)
//			{
//				// mask |= ((byte)(1<<i) & Convert.ToByte(learning_days[i]));
//				if (days[i])
//					mask |= (byte)(1 << i);
//			}
//			Group.learning_days = mask;
//		}
//		void Exstract()
//		{
//			for (int i = 0; i < 7; i++)
//			{
//				if ((Group.learning_days >> i & 1) != 0)
//					days[i] = true;
//			}
//		}
//		public void OnGet()
//        {
//			Exstract();
//        }
//    }
//}
