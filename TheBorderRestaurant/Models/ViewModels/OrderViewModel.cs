using System.Collections.Generic;
using TheBorderRestaurant.Models.DomainModels;

namespace TheBorderRestaurant.Models.ViewModels
{
    public class OrderViewModel
    {
        #region Properties

        public int OrderId { get; set; }
        public List<FoodOrderItem> Items { get; set; }

        #endregion
    }
}