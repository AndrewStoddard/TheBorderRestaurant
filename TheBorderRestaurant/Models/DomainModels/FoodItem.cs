using System.ComponentModel.DataAnnotations;

namespace TheBorderRestaurant.Models.DomainModels
{
    public class FoodItem
    {
        #region Properties

        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public double Price { get; set; }
        [Required] public string ImageName { get; set; }

        #endregion
    }
}