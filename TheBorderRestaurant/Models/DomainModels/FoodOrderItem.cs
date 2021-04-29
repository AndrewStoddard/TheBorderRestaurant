using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBorderRestaurant.Models.DomainModels
{
    public class FoodOrderItem
    {
        #region Properties

        public int Id { get; set; }
        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
        public int Quantity { get; set; }

        [Required] public virtual ICollection<FoodOrder> FoodOrders { get; set; }

        #endregion
    }
}