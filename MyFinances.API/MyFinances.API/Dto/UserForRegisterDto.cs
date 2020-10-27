using System.ComponentModel.DataAnnotations;

namespace MyFinances.API.Dto
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Hasło musi zawierać od 6 do 12 znaków")]
        public string Password { get; set; }
    }
}
