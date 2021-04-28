// ***********************************************************************
// Author           : Andrew Stoddard
// Created          : 04-27-2021
//
// Last Modified By : Andrew Stoddard
// Last Modified On : 04-27-2021
// ***********************************************************************

using TheBorderRestaurant.Models.DomainModels;

namespace TheBorderRestaurant.Models.DataLayer
{
    /// <summary>
    ///     Interface IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        #region Properties

        /// <summary>
        ///     Gets the food orders.
        /// </summary>
        /// <value>The food orders.</value>
        IRepository<FoodOrder> FoodOrders { get; }

        /// <summary>
        ///     Gets the food order items.
        /// </summary>
        /// <value>The food order items.</value>
        IRepository<FoodOrderItem> FoodOrderItems { get; }

        /// <summary>
        ///     Gets the food items.
        /// </summary>
        /// <value>The food items.</value>
        IRepository<FoodItem> FoodItems { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Saves this instance.
        /// </summary>
        void Save();

        #endregion
    }
}