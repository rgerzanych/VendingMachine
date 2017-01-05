using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Extensions;

namespace VendingMachine.Business
{
	public class VendingMachineStore : IVendingMachineStore
	{
		public void AppendProduct(int number, string productName, Money price, int amount = 1)
		{
			throw new NotImplementedException();
		}

		public void UpdateProductAmount(int number, int amount)
		{
			throw new NotImplementedException();
		}

		public void ClearProducts()
		{
			throw new NotImplementedException();
		}

		public IDictionary<int, Product> GetProducts()
		{
			throw new NotImplementedException();
		}

		public void RemoveProduct(int number)
		{
			throw new NotImplementedException();
		}

		public Product GetProduct(int number)
		{
			throw new NotImplementedException();
		}
	}
}
