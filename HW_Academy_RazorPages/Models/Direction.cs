using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_Academy_RazorPages.Models
{
	public class Direction
	{
		[Key]
		[Column(TypeName = "tinyint")]
		public int direction_id { get; set; }
		[Required]
		public string direction_name { get; set; }

		public ICollection<Group> Groups { get; set; }

	}
}
