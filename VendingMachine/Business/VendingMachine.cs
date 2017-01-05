using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Extensions;

namespace VendingMachine.Business
{
	public class VendingMachine : IVendingMachine
	{
		private Money _currentBallance;
		private readonly IVendingMachineStore _store;

		public VendingMachine(IVendingMachineStore store) : this(store, new Money(0, 0))
		{
			
		}

		public VendingMachine(IVendingMachineStore store, Money currentBallance)
		{
			_store = store;
			_currentBallance = currentBallance;
		}

		public string Manufacturer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Money Amount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEnumerable<Product> SoldProducts
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public IDictionary<int, Product> Store
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void InsertCoin(Money amount)
		{
			var denominatedBallance = _currentBallance.Denominate();
			var denominatedAmount = amount.Denominate();

			_currentBallance.Parse(denominatedBallance + denominatedAmount);
		}

		public Money ReturnMoney()
		{
			throw new NotImplementedException();
		}

		public Product BuyProduct(int productNumber)
		{
			var product = _store.GetProduct(productNumber);

			if (_currentBallance.Denominate() < product.Price.Denominate())
			{
				throw new InvalidOperationException("Not enough money to buy this product");
			}

			return product;
		}
	}
}
