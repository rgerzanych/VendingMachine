using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Business;

namespace VendingMachine.Extensions
{
	public static class MoneyExtensions
	{
		public static int Denominate(this Money money)
		{
			return money.Euros*100 + money.Cents;
		}

		public static Money Parse(this Money money, int value)
		{
			// euros
			var euros = value / 100;

			// cents
			var cents = value % 100;

			return new Money(euros, cents);
		}

		public static string ToDisplayString(this Money money)
		{
			return string.Format("{0} euros {1:D2} cents", money.Euros, money.Cents);
		}
	}
}
