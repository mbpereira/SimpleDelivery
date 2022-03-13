using Core.Orders.Contracts;
using Data.Entities.Catalog;
using Data.Entities.Sale;
using Data.Repositories.Catalog.Contracts;
using Data.Repositories.Sale.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Orders
{
    public class OrdersHandler : IOrdersHandler
    {
        private readonly IOrdersRepository _orders;
        private readonly ICustomersRepository _customers;
        private readonly IProductsRepository _products;

        public OrdersHandler(IOrdersRepository orders, ICustomersRepository customers, IProductsRepository products)
        {
            _orders = orders;
            _customers = customers;
            _products = products;
        }

        public async Task Create(Order order)
        {
            order.CreatedAt = System.DateTime.Now.Date;
            await ValidateBasicData(order);

            foreach (var item in order.Itens)
            {
                var product = await _products.GetByKey(item.ProductId);

                ValidateItem(item, product);

                if (item.UnitCost <= 0)
                    item.UnitCost = product.Cost;
                if (item.UnitPrice <= 0)
                    item.UnitPrice = product.SalePrice;

                if (!order.WasApproved()) continue;

                product.Stock -= item.Quantity;
                await _products.Update(product);
            }

            await _orders.Add(order);
        }

        private void ValidateItem(OrderItem item, Product product)
        {
            var errs = new List<string>();

            if (product == null)
                errs.Add($"invalid product id {item.ProductId}");

            if ((product != null) && (item.Quantity > product.Stock))
                errs.Add($"insufficient stock for product {product.Description}");

            ThrowExceptionIfNeed(errs);
        }

        private async Task ValidateBasicData(Order order)
        {
            var errs = new List<string>();
            var customer = await _customers.GetByKey(order.CustomerId);

            if (customer == null)
                errs.Add($"invalid customer id {order.CustomerId}");

            if (order.IsCanceled())
                errs.Add("new order's can't be canceled");

            ThrowExceptionIfNeed(errs);
        }

        private void ThrowExceptionIfNeed(List<string> errs)
        {
            if (errs.Count > 0) throw new System.Exception(string.Concat("Errors Occurred: ", string.Join(", ", errs)));
        }
    }
}
