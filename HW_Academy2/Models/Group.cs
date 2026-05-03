using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_Academy2.Models
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

		[NotMapped]
		public bool days1 { get; set; }
		[NotMapped]
		public bool days2 { get; set; }
		[NotMapped]
		public bool days3 { get; set; }
		[NotMapped]
		public bool days4 { get; set; }
		[NotMapped]
		public bool days5 { get; set; }
		[NotMapped]
		public bool days6 { get; set; }
		[NotMapped]
		public bool days7 { get; set; }

		// Navigation properties
		public Direction? Direction { get; set; }

		public ICollection<Student>? Students { get; set; }
	}
}
