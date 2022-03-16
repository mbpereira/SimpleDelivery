using Core.Orders;
using Core.Test.Orders.Fakes;
using Data.Entities.Catalog;
using Data.Entities.Sale;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test.Orders
{
    public class OrdersHandlerCreateTest
    {
        [Fact]
        public async Task ShouldThrowsExceptionWhenProvidedDataIsNotValid()
        {
            var order = new Order()
            {

                CustomerId = 99,
                Status = OrderStatus.Canceled,
            };

            var orders = new List<Order>();
            var customers = new List<Customer>();
            var products = new List<Product>();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Create(order));
            Assert.Contains("customer id", ex.Message);
            Assert.Contains("invalid status", ex.Message);
        }

        [Fact]
        public async Task ShouldThrowsExceptionWhenStockIsInsufficient()
        {
            var order = GetOrderToValidateInsufficientStock();

            var orders = new List<Order>();
            var customers = GetDefaultCustomers();
            var products = GetProductsToValidateInsufficientStock();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Create(order));
            Assert.Contains("insufficient stock", ex.Message);
        }

        private static List<Product> GetProductsToValidateInsufficientStock()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 1
                },
                new Product()
                {
                    Id = 2,
                    Stock = 2
                }
            };
        }

        private static List<Customer> GetDefaultCustomers()
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Id = 99
                }
            };
        }

        private static Order GetOrderToValidateInsufficientStock()
        {
            return new Order()
            {
                CustomerId = 99,
                Status = OrderStatus.Approved,
                Itens = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        ProductId = 1,
                        Quantity = 2,
                    },
                    new OrderItem()
                    {
                        ProductId = 2,
                        Quantity = 1
                    }
                }
            };
        }

        [Fact]
        public async Task ShouldThrowsExceptionWhenInvalidProductWasInformed()
        {
            var order = GetOrderToValidateInvalidProduct();

            var orders = new List<Order>();
            var customers = GetDefaultCustomers();
            var products = GetProductsToValidateInvalidProduct();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Create(order));
            Assert.Contains("invalid product", ex.Message);
        }

        private static List<Product> GetProductsToValidateInvalidProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 1
                }
            };
        }

        private static Order GetOrderToValidateInvalidProduct()
        {
            return new Order()
            {
                CustomerId = 99,
                Status = OrderStatus.Approved,
                Itens = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        ProductId = 2,
                        Quantity = 1
                    }
                }
            };
        }

        [Fact]
        public async Task ShouldUpdateProductStock()
        {
            var order = GetOrderToValidateProductStockUpdate();

            var orders = new List<Order>();
            var customers = GetDefaultCustomers();
            var products = GetProductsToValidateProductStockUpdate();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));
            await handler.Create(order);

            var item = order.Itens[0];
            var product = products[0];
            Assert.Equal(5, item.UnitCost);
            Assert.Equal(10, item.UnitPrice);
            Assert.Equal(0, product.Stock);
        }

        private static List<Product> GetProductsToValidateProductStockUpdate()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 5,
                    Cost = 5,
                    SalePrice = 10
                }
            };
        }

        private static Order GetOrderToValidateProductStockUpdate()
        {
            return new Order()
            {
                CustomerId = 99,
                Status = OrderStatus.Approved,
                Itens = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        ProductId = 1,
                        Quantity = 5
                    }
                }
            };
        }

        [Fact]
        public async Task ShouldGetDefaultCostAndSalePriceWhenNotProvided()
        {
            var order = GetOrderToValidateDefaultCostAndPrice();

            var orders = new List<Order>();
            var customers = GetDefaultCustomers();
            var products = GetProductsToValidateDefaultCostAndPrice();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));
            await handler.Create(order);

            OrderItem item;
            item = order.Itens[0];
            Assert.Equal(5, item.UnitCost);
            Assert.Equal(10, item.UnitPrice);

            item = order.Itens[1];
            Assert.Equal(8, item.UnitCost);
            Assert.Equal(16, item.UnitPrice);
        }

        private static List<Product> GetProductsToValidateDefaultCostAndPrice()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 5,
                    Cost = 5,
                    SalePrice = 10
                },
                new Product()
                {
                    Id = 2,
                    Stock = 5,
                    Cost = 8,
                    SalePrice = 16
                }
            };
        }

        private static Order GetOrderToValidateDefaultCostAndPrice()
        {
            return new Order()
            {
                CustomerId = 99,
                Status = OrderStatus.Approved,
                Itens = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new OrderItem()
                    {
                        ProductId = 2,
                        Quantity = 2
                    }
                }
            };
        }
    }
}
