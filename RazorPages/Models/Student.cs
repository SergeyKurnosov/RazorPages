using System.ComponentModel.DataAnnotations;
namespace RazorPages.Models
{
	public class Student
	{
		public int Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public DateTime EnrollMentDate { get; set; }

		//NavigationProperties
		public ICollection<Enrollment> Enrollments { get; set; }
	}
}
