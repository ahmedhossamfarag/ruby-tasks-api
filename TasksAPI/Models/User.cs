using System.ComponentModel.DataAnnotations;

namespace TasksAPI.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Username is required")]
		[StringLength(50, MinimumLength = 6)]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password is required")]
		[StringLength(50, MinimumLength = 8)]
		public string Password { get; set; } = string.Empty;
	}
}
