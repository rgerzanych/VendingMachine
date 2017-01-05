using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Business
{
	public interface IVendingMachineStore
	{
		void AppendProduct(int number, string productName, Money price, int amount = 1);

		void UpdateProductAmount(int number, int amount);

		void ClearProducts();

		IDictionary<int, Product> GetProducts();

		void RemoveProduct(int number);

		Product GetProduct(int number);
	}
}
