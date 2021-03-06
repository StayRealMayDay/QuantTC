﻿using System;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    /// <inheritdoc cref="IMovingAverage" />
    /// <summary>
    /// Exponential Moving Average
    /// </summary>
    public class EMA : Indicator<double>, IMovingAverage
    {
        /// <inheritdoc />
        public EMA(IIndicator<double> source, int period)
        {
            Source = source;
            Period = period;
            Source.Update += Source_Update;
            Title = $"{Source}.EMA{Period}";
        }

        private void Source_Update()
        {
            var alpha = 2.0 / (Period + 1);
            Data.FillRange(Count, Source.Count,
                i => i > 0 ? alpha * (Source[i] - Data[i - 1]) + Data[i - 1] : Source[i]);
            FollowUp();
        }

        private IIndicator<double> Source { get; }
        private int Period { get; }
    }

    public static partial class X
    {
        public static EMA EMA(this IIndicator<double> source, int period) => new EMA(source, period);
    }
}