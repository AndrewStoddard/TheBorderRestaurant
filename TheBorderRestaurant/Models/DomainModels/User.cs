using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TheBorderRestaurant.Models.DomainModels
{
    public class User : IdentityUser
    {
        #region Properties

        [NotMapped] public IList<string> RoleNames { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string EmailAddress { get; set; }
        [Required] public string Street { get; set; }
        [Required] public string City { get; set; }
        [Required] public string State { get; set; }
        [Required] public string Zip { get; set; }

        #endregion
    }
}