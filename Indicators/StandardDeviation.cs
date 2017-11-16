﻿using System.Linq;
using QuantTC.Indicators.Generic;
using static System.Math;
using static QuantTC.Functions;

namespace QuantTC.Indicators
{
	/// <inheritdoc />
	/// <summary>
	/// Standard Deviation (devided by N - 1)
	/// </summary>
	public class StandardDeviation : Indicator<double>
	{
		/// <summary>
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="period">period</param>
		public StandardDeviation(IIndicator<double> source, int period)
		{
			Source = source;
			Period = period;
			Average = new SMA(Source, Period);
			Source.Update += Source_Update;
		}

		/// <summary>
		/// </summary>
		/// <param name="source">source</param>
		/// <param name="average">instanced SMA(period)</param>
		/// <param name="period">period</param>
		public StandardDeviation(IIndicator<double> source, IIndicator<double> average, int period)
		{
			Source = source;
			Period = period;
			Average = average;
			Source.Update += Source_Update;
		}

		private void Source_Update()
		{
			Data.FillRange(Count, Source.Count,
				i => i > 1
					? Sqrt(RangeRight(0, i).Take(Period).Select(ii => Source[ii] - Average[i]).Select(x => x * x).Sum() /
					       Min(Period - 1, i - 1))
					: 0);
			FollowUp();
		}

		public IIndicator<double> Source { get; }
		public IIndicator<double> Average { get; }
		public int Period { get; }
	}
}