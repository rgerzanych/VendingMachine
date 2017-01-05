using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Business
{
	public interface IVendingMachine
	{
		/// <summary>Vending machine manufacturer.</summary>
		string Manufacturer { get; }

		/// <summary>Amount of money inserted into vending machine. </summary>

		Money Amount { get; }

		/// <summary>Products that are sold.</summary>
		IEnumerable<Product> SoldProducts { get; }

		/// <summary>
		/// Vending machine store
		/// </summary>
		IDictionary<int, Product> Store { get; }

		/// <summary>Inserts the coin into vending machine.</summary>
		/// <param name="amount">Coin amount.</param>
		void InsertCoin(Money amount);

		/// <summary>Returns all inserted coins back to user.</summary>
		Money ReturnMoney();

		/// <summary>Buys product from list of product.</summary>
		/// <param name="productNumber">Product number in vending machine product list.</param>
		Product BuyProduct(int productNumber);
	}
}
