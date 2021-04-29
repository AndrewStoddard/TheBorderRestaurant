using System.ComponentModel.DataAnnotations;

namespace TheBorderRestaurant.Models.ViewModels
{
    public class LoginViewModel
    {
        #region Properties

        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        #endregion
    }
}