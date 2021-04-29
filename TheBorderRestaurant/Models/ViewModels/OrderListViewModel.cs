using System.Collections.Generic;
using TheBorderRestaurant.Models.DomainModels;

namespace TheBorderRestaurant.Models.ViewModels
{
    public class OrderListViewModel
    {
        #region Properties

        public List<FoodOrder> Orders { get; set; }

        /// <summary>
        ///     Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; set; }

        /// <summary>
        ///     Gets the page sizes.
        /// </summary>
        /// <value>The page sizes.</value>
        public int[] PageSizes { get; } = {1, 2, 3, 4, 5};

        #endregion
    }
}