namespace TheBorderRestaurant.Models.DomainModels
{
    public class FoodOrderItem
    {
        #region Properties

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
        public int FoodOrderId { get; set; }
        public FoodOrder FoodOrder { get; set; }
        public int Quantity { get; set; }

        #endregion
    }
}