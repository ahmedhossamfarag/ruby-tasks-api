using System.ComponentModel.DataAnnotations;

namespace TasksAPI.Models
{
	public enum Priority : byte
	{
		Low, Medium, High
	}

	public class TaskNote
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 4)]
		public string Title { get; set; } = string.Empty;

		[StringLength(100)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public Priority Priority { get; set; } = Priority.Low;

		[Required]
		public DateTime DueDate { get; set;} = DateTime.Now;
	}
}
