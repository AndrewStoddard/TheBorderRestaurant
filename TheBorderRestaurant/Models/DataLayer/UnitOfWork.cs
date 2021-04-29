using TheBorderRestaurant.Models.DomainModels;

namespace TheBorderRestaurant.Models.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Data members

        private readonly BorderContext context;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the food orders.
        /// </summary>
        /// <value>The food orders.</value>
        public IRepository<FoodOrder> FoodOrders { get; }

        /// <summary>
        ///     Gets the food order items.
        /// </summary>
        /// <value>The food order items.</value>
        public IRepository<FoodOrderItem> FoodOrderItems { get; }

        /// <summary>
        ///     Gets the food items.
        /// </summary>
        /// <value>The food items.</value>
        public IRepository<FoodItem> FoodItems { get; }

        #endregion

        #region Constructors

        public UnitOfWork(BorderContext context)
        {
            this.context = context;
            this.FoodOrderItems = new Repository<FoodOrderItem>(context);
            this.FoodOrders = new Repository<FoodOrder>(context);
            this.FoodItems = new Repository<FoodItem>(context);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Saves this instance.
        /// </summary>
        public void Save()
        {
            this.context.SaveChanges();
        }

        #endregion
    }
}