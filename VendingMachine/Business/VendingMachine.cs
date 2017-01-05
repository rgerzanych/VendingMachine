﻿using System;
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
		private Money _currentBalance;
		private readonly IVendingMachineStore _store;

		public VendingMachine(IVendingMachineStore store) : this(store, new Money(0, 0))
		{
			
		}

		public VendingMachine(IVendingMachineStore store, Money currentBallance)
		{
			_store = store;
			_currentBalance = currentBallance;
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
				return _currentBalance;
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
			var denominatedBallance = _currentBalance.Denominate();
			var denominatedAmount = amount.Denominate();

			_currentBalance.Parse(denominatedBallance + denominatedAmount);
		}

		public Money ReturnMoney()
		{
			throw new NotImplementedException();
		}

		public Product BuyProduct(int productNumber)
		{
			var product = _store.GetProduct(productNumber);
			var denominatedBallance = _currentBalance.Denominate();
			var denominatedProductPrice = product.Price.Denominate();

			if (denominatedBallance < denominatedProductPrice)
			{
				throw new InvalidOperationException("Not enough money to buy this product");
			}

			// update current ballance
			var change = denominatedBallance - denominatedProductPrice;
			_currentBalance = _currentBalance.Parse(change);

			return product;
		}
	}
}
