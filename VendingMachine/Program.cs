using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VendingMachine.Business;
using VendingMachine.Extensions;

namespace VendingMachine
{
	class Program
	{
		const string INCORRECT_COMMAND = "<Incorrect command>";

		private static Business.VendingMachine _vendingMachine;
		static void Main(string[] args)
		{
//			_vendingMachine = new Business.VendingMachine("Super Vending Machine", new VendingMachineStore(), GetInitialProductList());

//			ConsoleKeyInfo key;
//			do
//			{
//				Console.WriteLine(@"
//*******************************
//* Command menu:
//* F1 - Get available products
//* F2 - Insert money
//* F3 - Return money
//* F4 - Buy product
//* Esc - Exit
//*******************************");
//				key = Console.ReadKey();

//				try
//				{
//					switch (key.Key)
//					{
//						case ConsoleKey.F1:
//							ShowVendingMachineStore();
//							break;
//						case ConsoleKey.F2:
//							InsertMoney();
//							break;
//						case ConsoleKey.F3:
//							ReturnMoney();
//							break;
//						case ConsoleKey.F4:
//							BuyProduct();
//							break;
//						default:
//							if (key.Key != ConsoleKey.Escape)
//							{
//								Console.WriteLine(INCORRECT_COMMAND);
//							}
//							break;
//					}
//				}
//				catch (Exception ex)
//				{
//					Console.WriteLine(String.Format("\r\n[!!!] ERROR: {0}\r\n", ex.Message));
//				}
				
//				Console.WriteLine(String.Empty);
//			}
//			while (key.Key != ConsoleKey.Escape);
		}

		static void ShowVendingMachineStore()
		{
			//Console.WriteLine("== Store ==");
			//foreach (var productInStore in _vendingMachine.ProductsInStore)
			//{
			//	Console.WriteLine(String.Format("[{0}] {1}{2}\t= {3}", 
			//		productInStore.Key, 
			//		productInStore.Value.Name,
			//		productInStore.Value.Available == 0 ? " (N/A)" : "\t",
			//		productInStore.Value.Price.ToDisplayString()
			//	));
			//}
		}

		static void InsertMoney()
		{
			Console.WriteLine(@"
***************************************
* Please insert money 
* (Choose one of the following options: 
**** F1 - 10ȼ
**** F2 - 20ȼ
**** F3 - 50ȼ
**** F4 - 1 €
**** F5 - 2 €
**** (Esc - Back to command menu )
***************************************");
			ConsoleKeyInfo key;
			do
			{
				var insertedMoney = new Money();
				var isMoneyValid = true;
				Console.Write("Your input:");
				key = Console.ReadKey();

				switch (key.Key)
				{
					case ConsoleKey.F1:
						insertedMoney.Cents = 10;
						break;
					case ConsoleKey.F2:
						insertedMoney.Cents = 20;
						break;
					case ConsoleKey.F3:
						insertedMoney.Cents = 50;
						break;
					case ConsoleKey.F4:
						insertedMoney.Euros = 1;
						break;
					case ConsoleKey.F5:
						insertedMoney.Euros = 2;
						break;
					default:
						isMoneyValid = false;
						if (key.Key != ConsoleKey.Escape)
						{
							Console.WriteLine(INCORRECT_COMMAND);
						}
						break;
				}

				if (isMoneyValid)
				{
					_vendingMachine.InsertCoin(insertedMoney);

					// show total ballance
					Console.WriteLine(string.Format(@"
*******************************************
* Current ballance: {0}
*******************************************", 
					_vendingMachine.Amount.ToDisplayString()));
				}
			}
			while (key.Key != ConsoleKey.Escape);
		}

		static void ReturnMoney()
		{
			var returnedMoney = _vendingMachine.ReturnMoney();
			Console.WriteLine(String.Format(@"
***************************************
* Returned money: {0}
**************************************", 
				returnedMoney.ToDisplayString()));
		}

		static void BuyProduct()
		{
			Console.Write("Please, input product number and press ENTER:");
			var inputData = Console.ReadLine();

			int productNumber;
			Int32.TryParse(inputData, out productNumber);

			if (productNumber <= 0)
			{
				Console.WriteLine("\r\n[!!!] ERROR: Incorrect input data. Please, try again!");
				return;
			}

			var product = _vendingMachine.BuyProduct(productNumber);
			var change = _vendingMachine.ReturnMoney();

			Console.WriteLine(String.Format(@"
***********************************************
* Thank you for using our {0}!
***********************************************", _vendingMachine.Manufacturer));
			Console.WriteLine(String.Format("* Don't forget to pick your '{0}'{1}!", 
				product.Name, 
				change.Denominate() > 0 ? String.Format(" and change ({0})", change.ToDisplayString())
										: string.Empty));
			Console.WriteLine(@"
********************
* Have a nice day! *
********************");
		}

		static Product[] GetInitialProductList()
		{
			var productList = new List<Product>();

			productList.Add(new Product
			{
				Name = "Chewing Gum",
				Price = new Money() { Euros = 1, Cents = 50 },
				Available = 10
			});

			productList.Add(new Product
			{
				Name = "Coca Cola",
				Price = new Money() { Euros = 3, Cents = 10 }
			});

			productList.Add(new Product
			{
				Name = "Snickers",
				Price = new Money() { Euros = 2, Cents = 20 },
				Available = 1
			});

			productList.Add(new Product
			{
				Name = "Skittles",
				Price = new Money() { Euros = 5 },
				Available = 5
			});
			
			return productList.ToArray();
		}	
	}
}
