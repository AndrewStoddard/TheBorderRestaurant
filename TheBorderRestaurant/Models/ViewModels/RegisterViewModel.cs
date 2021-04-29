using System.ComponentModel.DataAnnotations;

namespace TheBorderRestaurant.Models.ViewModels
{
    public class RegisterViewModel
    {
        #region Properties

        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a street.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter a city.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a zip.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        #endregion
    }
}