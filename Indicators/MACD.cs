﻿using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
	/// <summary>
	/// Moving Average Convergence / Divergence
	/// </summary>
	/// <remarks>Composite Indicator</remarks>
	public class MACD
	{
		/// <summary>
		/// Construct a MACD, recommand (12, 26, 9)
		/// </summary>
		/// <param name="source">Source</param>
		/// <param name="fastPeriod">Fast EMA Period</param>
		/// <param name="slowPeriod">Slow EMA Period</param>
		/// <param name="diffPeriod">Diff EMA Period</param>
		public MACD(IIndicator<double> source, int fastPeriod, int slowPeriod, int diffPeriod)
		{
			Source = source;
			FastPeriod = fastPeriod;
			SlowPeriod = slowPeriod;
			DiffPeriod = diffPeriod;
			var Title = $"{Source}.MACD({FastPeriod}, {SlowPeriod}, {DiffPeriod})";
			Fast = new EMA(Source, FastPeriod){Title = Title + ".Fast"};
			Slow = new EMA(Source, SlowPeriod){Title = Title + ".Slow"};
			Diff = new BinaryOperation<double, double, double>(Fast, Slow, (f, s) => f - s){Title = Title + ".DIFF"};
			Dea = new EMA(Diff, DiffPeriod) {Title = Title + ".DEA"};
			Macd = new BinaryOperation<double, double, double>(Diff, Dea, (f, s) => 2 * (f - s)) {Title = Title + ".MACD"};
		}

		/// <summary>
		/// Fast Line
		/// </summary>
		public IIndicator<double> Fast { get; }

		/// <summary>
		/// Slow Line
		/// </summary>
		public IIndicator<double> Slow { get; }

		/// <summary>
		/// DIFF := Fast - Slow
		/// </summary>
		public IIndicator<double> Diff { get; }

		/// <summary>
		/// DEA := EMA(DIFF)
		/// </summary>
		public IIndicator<double> Dea { get; }

		/// <summary>
		/// MACD := 2 * (DIFF - DEA)
		/// </summary>
		public IIndicator<double> Macd { get; set; }

		private IIndicator<double> Source { get; }

		/// <summary>
		/// Fast EMA Period
		/// </summary>
		public int FastPeriod { get; }

		/// <summary>
		/// Slow EMA Period
		/// </summary>
		public int SlowPeriod { get; }

		/// <summary>
		/// Diff EMA Period
		/// </summary>
		public int DiffPeriod { get; }
	}
}