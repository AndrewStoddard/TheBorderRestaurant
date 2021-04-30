using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBorderRestaurant.Models.DomainModels
{
    public class FoodOrder
    {
        #region Properties

        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool IsComplete { get; set; }

        [Required] public virtual ICollection<FoodOrderItem> FoodOrderItems { get; set; }

        #endregion

        #region Methods

        public double Total()
        {
            var sum = 0.0;
            foreach (var orderItem in this.FoodOrderItems)
            {
                sum += orderItem.TotalPrice;
            }

            return sum;
        }

        #endregion
    }
}