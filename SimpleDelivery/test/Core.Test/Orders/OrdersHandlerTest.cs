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
    public class OrdersHandlerTest
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

            try
            {
                await handler.Create(order);
            }
            catch (Exception ex)
            {
                Assert.Contains("customer id", ex.Message);
                Assert.Contains("be canceled", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowsExceptionWhenStockIsInsufficient()
        {
            var order = new Order()
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

            var orders = new List<Order>();
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Id = 99
                }
            };
            var products = new List<Product>()
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

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            try
            {
                await handler.Create(order);
            }
            catch (Exception ex)
            {
                Assert.Contains("insufficient stock", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowsExceptionWhenInvalidProductWasInformed()
        {
            var order = new Order()
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

            var orders = new List<Order>();
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Id = 99
                }
            };
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 1
                }
            };

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));

            try
            {
                await handler.Create(order);
            }
            catch (Exception ex)
            {
                Assert.Contains("invalid product", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldUpdateProductStock()
        {
            var order = new Order()
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

            var orders = new List<Order>();
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Id = 99
                }
            };
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Stock = 5,
                    Cost = 5,
                    SalePrice = 10
                }
            };

            var handler = new OrdersHandler(new OrdersRepository(orders), new CustomersRepository(customers), new ProductsRepository(products));
            await handler.Create(order);

            var item = order.Itens[0];
            var product = products[0];
            Assert.Equal(5, item.UnitCost);
            Assert.Equal(10, item.UnitPrice);
            Assert.Equal(0, product.Stock);
        }

        [Fact]
        public async Task ShouldGetDefaultCostAndSalePriceWhenNotProvided()
        {
            var order = new Order()
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

            var orders = new List<Order>();
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Id = 99
                }
            };
            var products = new List<Product>()
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
    }
}
