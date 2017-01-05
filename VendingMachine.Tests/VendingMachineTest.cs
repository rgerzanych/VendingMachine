﻿using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using VendingMachine.Business;

namespace VendingMachine.Tests
{
	[TestFixture]
	public class VendingMachineTest
	{
		private IVendingMachine _vendingMachine;

		[Test]
		[TestCase(0, 0)]
		[TestCase(1, 0)]
		[TestCase(0, 10)]
		[TestCase(1, 50)]
		public void BuyProduct_NotEnoughMoney_ReturnsFailed(int euros, int cents)
		{
			//Arrange
			_vendingMachine = GetVendingMachine(new Money(euros, cents));

			//Act
			_vendingMachine.InsertCoin(new Money(1, 0));
			var ex = Assert.Catch<InvalidOperationException>(() => _vendingMachine.BuyProduct(1));

			//Assert
			StringAssert.Contains("Not enough money to buy this product", ex.Message);
		}

		[Test]
		public void BuyProduct_EnoughMoneyNoChange_ReturnsSuccess()
		{
			//Arrange
			_vendingMachine = GetVendingMachine(new Money(2, 50));

			//Act
			var product = _vendingMachine.BuyProduct(1);

			//Assert
			Assert.IsNotNull(product);
			Assert.AreEqual(product.Name, "Product 1");
			Assert.AreEqual(product.Price, new Money(2, 50));
		}

		[Test]
		public void BuyProduct_EnoughMoneyWithChange_ReturnsSuccess()
		{
			//Arrange
			_vendingMachine = GetVendingMachine(new Money(3, 0));

			//Act
			var product = _vendingMachine.BuyProduct(1);

			//Assert
			Assert.IsNotNull(product);
			Assert.AreEqual(product.Name, "Product 1");
			Assert.AreEqual(product.Price, new Money(2, 50));
			Assert.AreEqual(_vendingMachine.Amount, new Money(0, 50));
		}

		private Dictionary<int, Product> GetSampleProductsStore()
		{
			var _storeData = new Dictionary<int, Product>();
			_storeData.Add(0, new Product { Name = "Product 1", Price = new Money { Euros = 2, Cents = 50 }, Available = 3 });
			_storeData.Add(1, new Product { Name = "Product 2", Price = new Money { Euros = 0, Cents = 50 }, Available = 0 });
			_storeData.Add(4, new Product { Name = "Product 5", Price = new Money { Euros = 0, Cents = 50 }, Available = 10 });
			_storeData.Add(8, new Product { Name = "Product 9", Price = new Money { Euros = 0, Cents = 50 }, Available = 1 });
			_storeData.Add(9, new Product { Name = "Product 10", Price = new Money { Euros = 0, Cents = 50 }, Available = 100 });

			return _storeData;
		}

		private IVendingMachine GetVendingMachine(Money startBallance)
		{
			//todo: should be rewritten
			var fakeStore = new FakeVendingMachineStore();

			return new Business.VendingMachine(fakeStore, startBallance);
		}

		internal class FakeVendingMachineStore : IVendingMachineStore
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
				return new Product()
				{
					Name = "Product 1",
					Price = new Money(2, 50),
					Available = 10
				};
			}
		}
	}
}
