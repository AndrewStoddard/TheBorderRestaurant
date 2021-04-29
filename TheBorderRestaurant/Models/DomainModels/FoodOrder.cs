using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBorderRestaurant.Models.DomainModels
{
    public class FoodOrder
    {
        #region Properties

        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool IsComplete { get; set; }

        [Required] public virtual ICollection<FoodOrderItem> FoodOrderItems { get; set; }

        #endregion
    }
}