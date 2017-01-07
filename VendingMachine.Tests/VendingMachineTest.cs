using System;
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
			_vendingMachine.InsertCoin(new Money(0, 50));
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
			Assert.AreEqual("Product 1", product.Name);
			Assert.AreEqual(new Money(2, 50), product.Price);
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
			Assert.AreEqual("Product 1", product.Name);
			Assert.AreEqual(new Money(2, 50), product.Price);
			Assert.AreEqual(new Money(0, 50), _vendingMachine.Amount);
		}

		[Test]
		[TestCase(0, 0, 0, 0, 0, 0)]
		[TestCase(0, 0, 3, 20, 3, 20)]
		[TestCase(0, 0, 0, 10, 0, 10)]
		[TestCase(1, 0, 0, 0, 1, 0)]
		[TestCase(1, 0, 0, 50, 1, 50)]
		[TestCase(0, 10, 0, 0, 0, 10)]
		[TestCase(0, 10, 1, 10, 1, 20)]
		public void InsertCoin_CorrectBalance_ReturnsSuccess(int eurosInsert, int centsInsert, int eurosBalance, int centsBalance, int eurosExpected, int centsExpected)
		{
			//Arrange
			_vendingMachine = GetVendingMachine(new Money(eurosBalance, centsBalance));

			//Act
			_vendingMachine.InsertCoin(new Money(eurosInsert, centsInsert));

			//Assert
			Assert.AreEqual(eurosExpected, _vendingMachine.Amount.Euros);
			Assert.AreEqual(centsExpected, _vendingMachine.Amount.Cents);
		}

		[Test]
		public void ReturnMoney_BalanceIsNotEmpty_ReturnsSuccess()
		{
			//Arrange
			_vendingMachine = GetVendingMachine(new Money(1, 50));

			//Act
			var change = _vendingMachine.ReturnMoney();

			//Assert
			Assert.AreEqual(_vendingMachine.Amount.Euros, 0);
			Assert.AreEqual(_vendingMachine.Amount.Cents, 0);
			Assert.AreEqual(1, change.Euros);
			Assert.AreEqual(50, change.Cents);
		}

		[Test]
		public void ReturnMoney_BalanceIsEmpty_ReturnsFailed()
		{
			//Arrange
			_vendingMachine = GetVendingMachine();

			//Act
			var ex = Assert.Catch<InvalidOperationException>(() => _vendingMachine.ReturnMoney());

			//Assert
			StringAssert.Contains("Balance is empty. Nothing to return", ex.Message);
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


		private IVendingMachine GetVendingMachine()
		{
			return GetVendingMachine(new Money(0, 0));
		}
		private IVendingMachine GetVendingMachine(Money startBallance)
		{
			//todo: should be rewritten
			var fakeStore = new FakeVendingMachineStore();

			return new Business.VendingMachine("Fake Vending Machine", fakeStore, startBallance);
		}

		internal class FakeVendingMachineStore : IVendingMachineStore
		{
			public void AddProduct(int number, string productName, Money price, int amount = 1)
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

			public Product GetProductByNumber(int number)
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
