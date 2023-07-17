using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewsModels
{
	public class RegisterViewModel
	{
        public string FName { get; set; }
        public string LName { get; set; }

        [Required (ErrorMessage ="Email is Required ")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
        public string Emial { get; set; }

		[Required (ErrorMessage ="Passward is Required")]
		[DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage ="ConfermPassward is Required ")]
		[Compare("Password",ErrorMessage ="ConfermPassword dose not match Password" )]
		[DataType(DataType.Password)]
        public string ConfermPassword { get; set; }
        public bool IsAgree { get; set; }	 

    }
}