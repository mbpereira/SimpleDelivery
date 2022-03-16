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
    public class OrdersHandlerUpdateTest
    {
        [Fact]
        public async Task ShouldThrowsExceptionWhenOldOrderIsFinished()
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

            orders[0].Status = OrderStatus.Delivered;
            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Update(orders[0]));
            Assert.Contains("finished order", ex.Message);
        }

        [Fact]
        public async Task ShouldThrowsExceptionWhenUpdatedOrderStatusIsCanceled()
        {
            var orders = GetOrdersToValidateUpdateException();
            var customers = GetDefaultCustomers();
            var products = new List<Product>();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            var updatedOrder = orders[0].Clone();
            updatedOrder.Status = OrderStatus.Canceled;

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Update(updatedOrder));
            Assert.Contains("you should use [PATCH /orders/1/cancel]", ex.Message);
        }

        private static List<Order> GetOrdersToValidateUpdateException()
        {
            return new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 99,
                    Status = OrderStatus.Approved,
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

        [Fact]
        public async Task ShouldThrowsExceptionWhenStockIsInsufficient()
        {
            var order = GetOrderToValidateInsufficientStock();
            var orders = new List<Order>() { order };
            var customers = GetDefaultCustomers();
            var products = GetProductsToValidateInsufficientStock();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            var updated = order.Clone();
            updated.Status = OrderStatus.Approved;

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Update(updated));
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

        private static Order GetOrderToValidateInsufficientStock()
        {
            return new Order()
            {
                CustomerId = 99,
                Status = OrderStatus.Received,
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
        public async Task ShouldSyncProductStock()
        {
            var itens = GetOrderItemsToValidateProductStockSync();
            var orders = GetOrdersToValidateProductStockSync(itens);
            var updatedOrder = orders[0].Clone();
            updatedOrder.Itens = GetOrderItemsToValidateProductStockSync();
            updatedOrder.Itens[0].Quantity = 7;
            updatedOrder.Itens[1].Quantity = 1;
            updatedOrder.Itens.RemoveAt(2);
            updatedOrder.Itens.Add(new OrderItem()
            {
                ProductId = 4,
                Quantity = 2
            });

            var customers = GetDefaultCustomers();
            var products = GetProductsToValidateProductStockSync();

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));
            await handler.Update(updatedOrder);

            Assert.Equal(7, orders[0].Itens[0].Quantity);
            Assert.Equal(1, orders[0].Itens[1].Quantity);

            Assert.Equal(10, products[0].Stock);
            Assert.Equal(18, products[1].Stock);
            Assert.Equal(15, products[2].Stock);
            Assert.Equal(10, products[3].Stock);
        }

        private static List<Order> GetOrdersToValidateProductStockSync(List<OrderItem> itens)
        {
            return new List<Order>()
            {
                new Order()
                {
                    CustomerId = 99,
                    Status = OrderStatus.Approved,
                    Itens = itens
                }
            };
        }

        private static List<Product> GetProductsToValidateProductStockSync()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 12,
                },
                new Product()
                {
                    Id = 2,
                    Stock = 12,
                },
                new Product()
                {
                    Id = 3,
                    Stock = 12,
                },
                new Product()
                {
                    Id = 4,
                    Stock = 12,
                }
            };
        }

        private static List<OrderItem> GetOrderItemsToValidateProductStockSync()
        {
            return new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Id = 1,
                        ProductId = 1,
                        Quantity = 5
                    },
                    new OrderItem()
                    {
                        Id = 2,
                        ProductId = 2,
                        Quantity = 7
                    },
                    new OrderItem()
                    {
                        Id = 3,
                        ProductId = 3,
                        Quantity = 3
                    }
                };
        }
    }
}
