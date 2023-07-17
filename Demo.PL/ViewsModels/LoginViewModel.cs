using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewsModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Passward is Required")]
		[DataType(DataType.Password)]
		public string Password{ get; set; }

        public bool RememberMe { get; set; }
    }
}
