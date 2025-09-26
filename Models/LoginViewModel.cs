using System.ComponentModel.DataAnnotations;

namespace coba.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username wajib diisi.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password wajib diisi.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
