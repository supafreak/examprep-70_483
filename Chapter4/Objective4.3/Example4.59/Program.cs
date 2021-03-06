﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Example4._59
{
    class Program
    {
        static void Main(string[] args)
        {
            var orders = GetOrders();
            var averageNumberOfOrderLines = orders.Average(o => o.OrderLines.Count);
            Console.WriteLine("Average size of order lines: {0}", averageNumberOfOrderLines);
            Console.WriteLine();

            // Listing 4.60
            var result = from o in orders
                         from l in o.OrderLines
                         group l by l.Product into p
                         select new
                         {
                             Product = p.Key,
                             Amount = p.Sum(x => x.Amount)
                         };
            Console.WriteLine("Sold per product:");
            foreach (var item in result)
            {
                Console.WriteLine("{0} : {1}", item.Product.Description, item.Amount);
            }
            Console.WriteLine();

            // Listing 4.61
            var products = from o in orders
                           from ol in o.OrderLines
                           select ol.Product;
            string[] popularProductNames = { "A", "B" };
            var popularProducts = from p in products
                                  join n in popularProductNames on p.Description equals n
                                  select p;
            Console.WriteLine("Our popular products:\n{0}", string.Join("\n", popularProducts));
            Console.WriteLine();

            // Listing 4.62
            Console.WriteLine("First page orders:\n{0}", string.Join("\n", GetOrders(orders, 1, 2)));
            Console.WriteLine();
            Console.WriteLine("Second page orders:\n{0}", string.Join("\n", GetOrders(orders, 2, 2)));
            Console.WriteLine();


            Console.WriteLine();
            Console.Write("Press a key to exit ... ");
            Console.ReadKey();

        }

        private static IEnumerable<Order> GetOrders(List<Order> orders, int pageIndex, int pageSize)
        {
            var pagedOrders = orders
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            return pagedOrders;
        }

        private static List<Order> GetOrders()
        {
            var product1 = new Product() { Description = "A", Price = 1 };
            var product2 = new Product() { Description = "B", Price = 2 };
            var product3 = new Product() { Description = "C", Price = 3 };
            var orders = new List<Order>()
            {
                new Order()
                { 
                    OrderLines = new List<OrderLine>() 
                    { 
                        new OrderLine() 
                        { 
                            Amount = 1, 
                            Product = product1
                        },
                        new OrderLine() 
                        { 
                            Amount = 2, 
                            Product = product1
                        }
                    }
                },
                new Order()
                { 
                    OrderLines = new List<OrderLine>() 
                    { 
                        new OrderLine() 
                        { 
                            Amount = 3, 
                            Product = product2
                        }
                    }
                },
                new Order()
                { 
                    OrderLines = new List<OrderLine>() 
                    { 
                        new OrderLine() 
                        { 
                            Amount = 1, 
                            Product = product2
                        },
                        new OrderLine() 
                        { 
                            Amount = 1, 
                            Product = product2
                        },
                        new OrderLine() 
                        { 
                            Amount = 1, 
                            Product = product3
                        }
                    }
                },
            };

            return orders;
        }
    }

    public class Product
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Description={Description},Price={Price}";
        }
    }

    public class OrderLine
    {
        public int Amount { get; set; }

        public Product Product { get; set; }

        public override string ToString()
        {
            return $"Amount={Amount},Product=\n\t\t{Product}";
        }
    }

    public class Order
    {
        public List<OrderLine> OrderLines { get; set; }

        public override string ToString()
        {
            string result = "Order:\n";
            foreach (var order in OrderLines)
            {
                result += "\t" + order + "\n";
            }
            return result;
        }
    }
}
