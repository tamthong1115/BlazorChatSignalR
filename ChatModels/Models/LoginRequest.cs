using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChatModels.Models
{
	public class LoginRequest
	{
		[Required]
		[StringLength(50)]
		[EmailAddress]
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;

	}
}
