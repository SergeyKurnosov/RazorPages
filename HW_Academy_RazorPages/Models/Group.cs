using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_Academy_RazorPages.Models
{
	public class Group
	{
		[Key]
		public int group_id { get; set; }

		[Required]
		public string group_name { get; set; }

		[Required]
		[Column(TypeName = "tinyint")]
		[ForeignKey("Direction")]
		public int direction { get; set; }

		[Column(TypeName = "tinyint")]
		public int learning_days { get; set; }


		public TimeOnly start_time { get; set; }

		// Navigation properties
		public Direction? Direction { get; set; }

		public ICollection<Student> Students { get; set; } = default!;
	}
}
