using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatModels.Models
{
	public class RegisterRequest
	{
		[Required]
		[StringLength(50)]
		public string Email { get; set; } = null!;

		[Required]
		[StringLength(25)]
		public string FirstName { get; set; } = null!;

		[Required]
		[StringLength(25)]
		public string LastName { get; set; } = null!;

		[Required]
		[StringLength(50)]
		public string Password { get; set; } = null!;

		[Required]
		[StringLength(50)]
		public string ConfirmPassword{ get; set; } = null!;
	}
}
