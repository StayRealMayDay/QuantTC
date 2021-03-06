﻿using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators.Collectors
{
    /// <inheritdoc />
    public class Cash : Indicator<double>
    {
        /// <inheritdoc />
        public Cash(IIndicator<IOrder> orders)
        {
            Orders = orders;
            Orders.Update += OrdersOnUpdate;
        }

        private void OrdersOnUpdate()
        {
            Data.FillRange(Count, Orders.Count,
                i => (i > 0 ? Data[i - 1] : 0) + (Orders[i].Direction == OrderDirection.Buy ? -1 : 1) * Orders[i].Lots *
                     Orders[i].Price);
            FollowUp();
        }

        private IIndicator<IOrder> Orders { get; }
    }
}