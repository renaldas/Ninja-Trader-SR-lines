#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
using System.IO;
using System.Globalization;
#endregion

// This namespace holds indicators in this folder and is required. Do not change it.
namespace NinjaTrader.NinjaScript.Indicators
{
	public class ContextTurtleSRPLines : Indicator
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description					= "";
				Name						= "Context Turtle S/R Lines";
				IsOverlay					= true;
				IsSuspendedWhileInactive	= true;
			}
		}

		protected override void OnBarUpdate()
		{
			string pattern = "*.txt";
			var dirInfo = new DirectoryInfo(NinjaTrader.Core.Globals.UserDataDir);
			var file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).First();

			if (file != null) {

			string readText = File.ReadAllText(file.FullName);

				if (!string.IsNullOrEmpty(readText))
				{
					string[] split = readText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

					foreach (string s in split)
					{
						string[] valList = s.Split(new char[] { ',' });

						if (valList.Length == 4)
						{
							var lineVal = double.Parse(valList[0], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
							var lineVal1 = double.Parse(valList[1], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);

							Draw.Text(this, "Text" + lineVal + "_" + lineVal1, false, valList[3], 0, lineVal1, 10, valList[2] == "S" ? Brushes.Green : Brushes.Red,
								ChartControl.Properties.LabelFont, TextAlignment.Center,
								valList[2] == "S" ? Brushes.Green : Brushes.Red, Brushes.LightGray, 40);

							Rectangle myRec = Draw.Rectangle(this, "Rectangle" + lineVal + "_" + lineVal1, false,
								DateTime.Now.AddDays(-1), lineVal1, DateTime.Now.AddHours(8), lineVal,
								valList[2] == "S" ? Brushes.Green : Brushes.Red, null, 40);
						}
					}
				}
			}			
		}

		#region Properties
		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(ResourceType = typeof(Custom.Resource), Name = "Period", GroupName = "NinjaScriptParameters", Order = 0)]
		public int Period
		{ get; set; }
		#endregion
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private ContextTurtleSRPLines[] cacheContextTurtleSRPLines;
		public ContextTurtleSRPLines ContextTurtleSRPLines(int period)
		{
			return ContextTurtleSRPLines(Input, period);
		}

		public ContextTurtleSRPLines ContextTurtleSRPLines(ISeries<double> input, int period)
		{
			if (cacheContextTurtleSRPLines != null)
				for (int idx = 0; idx < cacheContextTurtleSRPLines.Length; idx++)
					if (cacheContextTurtleSRPLines[idx] != null && cacheContextTurtleSRPLines[idx].Period == period && cacheContextTurtleSRPLines[idx].EqualsInput(input))
						return cacheContextTurtleSRPLines[idx];
			return CacheIndicator<ContextTurtleSRPLines>(new ContextTurtleSRPLines(){ Period = period }, input, ref cacheContextTurtleSRPLines);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.ContextTurtleSRPLines ContextTurtleSRPLines(int period)
		{
			return indicator.ContextTurtleSRPLines(Input, period);
		}

		public Indicators.ContextTurtleSRPLines ContextTurtleSRPLines(ISeries<double> input , int period)
		{
			return indicator.ContextTurtleSRPLines(input, period);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.ContextTurtleSRPLines ContextTurtleSRPLines(int period)
		{
			return indicator.ContextTurtleSRPLines(Input, period);
		}

		public Indicators.ContextTurtleSRPLines ContextTurtleSRPLines(ISeries<double> input , int period)
		{
			return indicator.ContextTurtleSRPLines(input, period);
		}
	}
}

#endregion
