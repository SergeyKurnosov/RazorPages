using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_Academy2.Models
{
	public class Student : Human
	{
		[Key]
		public int stud_id { get; set; }
		[Required]
		[ForeignKey("Group")]
		public int group { get; set; }

		// Navigation Properties
		public Group? Group { get; set; } = default!;
	}
}
