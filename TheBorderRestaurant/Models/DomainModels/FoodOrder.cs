using System;

namespace TheBorderRestaurant.Models.DomainModels
{
    public class FoodOrder
    {
        #region Properties

        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDateTime { get; set; }

        #endregion
    }
}