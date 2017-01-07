using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VendingMachine.Business;

namespace VendingMachine.Tests
{
	[TestFixture]
	public class VendingMachineStoreTest
	{
		[Test]
		public void AddNewProduct_EmptyStore_ReturnSuccess()
		{
			//Arrange
			var store = new VendingMachineStore();

			//Act
			store.AddProduct(2, "Test Product 1", new Money(2, 0), 5);
			var products = store.GetProducts();

			//Assert
			Assert.AreEqual(1, products.Count);
			Assert.AreEqual("Test Product 1", products[2].Name);
		}

		[Test]
		public void AddNewProduct_NonEmptyStore_ReturnSuccess()
		{
			//Arrange
			var storeProducts = new List<Product>();
			storeProducts.Add(new Product()
			{
				Name = "Test Product 1",
				Price = new Money(1, 50),
				Available = 3
			});
			
			var store = new VendingMachineStore(storeProducts);

			//Act
			store.AddProduct(2, "Test Product 2", new Money(2, 0), 5);
			var products = store.GetProducts();

			//Assert
			Assert.AreEqual(products.Count, 2);
			Assert.AreEqual("Test Product 1", products[1].Name);
			Assert.AreEqual("Test Product 2", products[2].Name);
		}

		[Test]
		[TestCase(-1, "Some product", 0, 0, 1, "Product number cannot be negative or zero")]
		[TestCase(0, "Some product", 0, 0, 1, "Product number cannot be negative or zero")]
		[TestCase(1, "", 0, 0, 0, "Product name cannot be empty")]
		[TestCase(1, "Some product", 0, 0, 1, "Product price cannot be negative or zero")]
		[TestCase(1, "Some product", -2, 0, 1, "Product price cannot be negative or zero")]
		[TestCase(1, "Some product", 0, -10, 1, "Product price cannot be negative or zero")]
		[TestCase(1, "Some product", -2, -50, 1, "Product price cannot be negative or zero")]
		[TestCase(1, "Some product", 2, 50, 0, "Product amount should larger than 0")]
		[TestCase(1, "Some product", 2, 50, -1, "Product amount should larger than 0")]
		public void AddNewProduct_InvalidInputData_ReturnFailed(int productNumber, string productName, 
			int productPriceEuros, int productPriceCents, int productAmount, string expectedException)
		{
			//Arrange
			var store = new VendingMachineStore();

			//Act
			var ex = Assert.Catch<InvalidOperationException>(() => store.AddProduct(productNumber, productName, new Money(productPriceEuros, productPriceCents), productAmount));
			
			//Assert
			StringAssert.Contains(expectedException, ex.Message);
		}
	}
}
