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
    public class OrdersHandlerCancelTest
    {
        [Fact]
        public async Task ShouldFixCurrentStockOfProductsAndSetOrderAsCanceled()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 99,
                    Status = OrderStatus.Approved,
                    Itens = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Id = 1,
                            Quantity = 3,
                            ProductId = 1
                        },
                        new OrderItem()
                        {
                            Id = 2,
                            Quantity = 7,
                            ProductId = 2,
                        },
                        new OrderItem()
                        {
                            Id = 3,
                            Quantity = 1,
                            ProductId = 3
                        }
                    }
                }
            };

            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Id = 99
                },

            };

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 3
                },
                new Product()
                {
                    Id = 2,
                    Stock = 1
                },
                new Product()
                {
                    Id = 3,
                    Stock = 2
                }
            };

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            await handler.Cancel(orders[0].Id);
            
            Assert.Equal(6, products[0].Stock);
            Assert.Equal(8, products[1].Stock);
            Assert.Equal(3, products[2].Stock);
            Assert.Equal(OrderStatus.Canceled, orders[0].Status);
        }

        [Fact]
        public async Task ShouldThrowsExceptionWhenOrderIsFinished()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 99,
                    Status = OrderStatus.Delivered,
                }
            };

            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Id = 99
                },

            };

            var products = new List<Product>();
            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            Exception ex;
            ex = await Assert.ThrowsAsync<Exception>(() => handler.Cancel(orders[0].Id));
            Assert.Contains("finished order", ex.Message);

            orders[0].Status = OrderStatus.Canceled;
            ex = await Assert.ThrowsAsync<Exception>(() => handler.Cancel(orders[0].Id));
            Assert.Contains("finished order", ex.Message);
        }
    }
}
