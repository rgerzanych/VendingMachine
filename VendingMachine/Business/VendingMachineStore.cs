using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Extensions;

namespace VendingMachine.Business
{
	public class VendingMachineStore : IVendingMachineStore
	{
		private Dictionary<int, Product> _store;

		public VendingMachineStore() : this(new List<Product>())
		{
			
		}
		 
		public VendingMachineStore(ICollection<Product> initialProducts)
		{
			_store = new Dictionary<int, Product>();
			InitializeStore(initialProducts);
		}

		public void AddProduct(int number, string productName, Money price, int amount = 1)
		{
			ValidateInput(number, productName, price, amount);

			var product = new Product()
			{
				Name = productName,
				Price = price,
				Available = amount
			};

			_store.Add(number, product);
		}

		public void UpdateProductAmount(int number, int amount)
		{
			throw new NotImplementedException();
		}

		public void ClearProducts()
		{
			_store.Clear();
		}

		public IDictionary<int, Product> GetProducts()
		{
			return _store;
		}

		public void RemoveProduct(int number)
		{
			CheckProductInStore(number);

			_store.Remove(number);
		}

		public Product GetProductByNumber(int number)
		{
			CheckProductInStore(number);

			return _store[number];
		}

		private void InitializeStore(IEnumerable<Product> products)
		{
			_store.Clear();

			var storeNumber = 1;
			foreach (var product in products)
			{
				_store.Add(storeNumber, product);
				storeNumber++;
			}
		}

		private void ValidateInput(int productNumber, string productName, Money price, int amount)
		{
			if (productNumber <= 0)
			{
				throw new InvalidOperationException("Product number cannot be negative or zero");
			}

			if (string.IsNullOrEmpty(productName))
			{
				throw new InvalidOperationException("Product name cannot be empty");
			}

			if ((price.Euros < 0) || (price.Cents < 0) || (price.Euros == 0 && price.Cents == 0))
			{
				throw new InvalidOperationException("Product price cannot be negative or zero");
			}

			if (amount <= 0)
			{
				throw new InvalidOperationException("Product amount should larger than 0");
			}
		}

		private void CheckProductInStore(int number)
		{
			if (!_store.ContainsKey(number))
			{
				throw new InvalidOperationException(string.Format("Product number {0} is not exists", number));
			}
		}
	}
}
