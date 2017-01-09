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

		[Test]
		public void GetProductsList_NonEmptyStore_ReturnSuccess()
		{
			//Arrange
			var storeProducts = new List<Product>();
			storeProducts.Add(new Product()
			{
				Name = "Test Product 1",
				Price = new Money(1, 50),
				Available = 3
			});
			storeProducts.Add(new Product()
			{
				Name = "Test Product 2",
				Price = new Money(0, 50),
				Available = 3
			});
			storeProducts.Add(new Product()
			{
				Name = "Test Product 3",
				Price = new Money(2, 50),
				Available = 3
			});

			var store = new VendingMachineStore(storeProducts);

			//Act
			var products = store.GetProducts();

			//Assert
			Assert.AreEqual(products.Count, 3);
			Assert.AreEqual("Test Product 1", products[1].Name);
			Assert.AreEqual("Test Product 2", products[2].Name);
			Assert.AreEqual("Test Product 3", products[3].Name);
		}

		[Test]
		public void ClearProductsInStore_NonEmptyStore_ReturnSuccess()
		{
			//Arrange
			var storeProducts = new List<Product>();
			storeProducts.Add(new Product()
			{
				Name = "Test Product 1",
				Price = new Money(1, 50),
				Available = 3
			});
			storeProducts.Add(new Product()
			{
				Name = "Test Product 2",
				Price = new Money(0, 50),
				Available = 3
			});
			storeProducts.Add(new Product()
			{
				Name = "Test Product 3",
				Price = new Money(2, 50),
				Available = 3
			});

			var store = new VendingMachineStore(storeProducts);

			//Act
			store.ClearProducts();
			var products = store.GetProducts();

			//Assert
			Assert.AreEqual(products.Count, 0);
		}

		[Test]
		[TestCase(false)]
		[TestCase(true)]
		public void GetProductFromStore_NoProductInStore_ReturnFailed(bool isStoreEmpty)
		{
			//Arrange
			VendingMachineStore store;

			var storeProducts = new List<Product>();
			if (!isStoreEmpty)
			{
				storeProducts.Add(new Product()
				{
					Name = "Test Product 1",
					Price = new Money(1, 50),
					Available = 3
				});
				storeProducts.Add(new Product()
				{
					Name = "Test Product 2",
					Price = new Money(0, 50),
					Available = 3
				});
				storeProducts.Add(new Product()
				{
					Name = "Test Product 3",
					Price = new Money(2, 50),
					Available = 3
				});
			}

			store = new VendingMachineStore(storeProducts);

			//Act
			var ex = Assert.Catch<InvalidOperationException>(() => store.GetProductByNumber(4));

			//Assert
			StringAssert.Contains("Product number 4 is not exists", ex.Message);
		}

		[Test]
		public void GetProductFromStore_ProductExistsInStore_ReturnSuccess()
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
			var product = store.GetProductByNumber(1);

			//Assert
			Assert.AreEqual(product.Name, "Test Product 1");
			Assert.AreEqual(product.Price, new Money(1, 50));
			Assert.AreEqual(product.Available, 3);
		}

		[Test]
		public void RemoveProductFromStore_ProductIsNotExist_ReturnFailed()
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
			var ex = Assert.Catch<InvalidOperationException>(() => store.RemoveProduct(2));

			//Assert
			StringAssert.Contains("Product number 2 is not exists", ex.Message);
		}

		[Test]
		public void RemoveProductFromStore_ProductExists_ReturnSuccess()
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
			store.RemoveProduct(1);
			var products = store.GetProducts();

			//Assert
			Assert.False(products.ContainsKey(1));
		}
	}
}
