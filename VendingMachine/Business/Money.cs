using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Business
{
	public struct Money
	{
		public Money(int euros, int cents)
		{
			Euros = euros;
			Cents = cents;
		}

		public int Euros { get; set; }

		public int Cents { get; set; }
	}
}
