using Data;
using Data.Entities.Reports;
using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : BaseController
    {
        private readonly IOrdersRepository _orders;
        private readonly SimpleDeliveryContext _context;

        public ReportsController(IOrdersRepository orders, SimpleDeliveryContext context)
        {
            _orders = orders;
            _context = context;
        }

        [ProducesResponseType(typeof(IList<SalePerformance>), statusCode: 200)]
        public async Task<IActionResult> Sales([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                var validStatus = new OrderStatus[] { OrderStatus.Delivered };
                var orders = await _orders
                    .GetByInterval(from, to, validStatus);

                return Ok(
                    orders.Select(o => new SalePerformance()
                    {
                        Id = o.Id,
                        Date = o.Date,
                        CustomerId = o.CustomerId,
                        Customer = o.Customer.Name,
                        ShipmentValue = o.ShipmentValue,
                        Status = o.Status,
                        GrossTotal = o.GrossTotal,
                        NetTotal = o.NetTotal,
                        TotalCost = o.TotalCost,
                        TotalDiscount = o.TotalDiscount,
                        Profit = o.Profit,
                        PercProfit = o.PercProfit
                    }).ToList()
               );
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [ProducesResponseType(typeof(IList<ProductSold>), statusCode: 200)]
        public async Task<IActionResult> BestProducts([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                return Ok(await _context.ProductsSold.FromSqlInterpolated($"select * from \"ProductsSold\"({from.Date}, {to.Date})").ToListAsync());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [ProducesResponseType(typeof(IList<CustomerPurchases>), statusCode: 200)]
        public async Task<IActionResult> BestCustomers([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                return Ok(await _context.CustomerPurchases.FromSqlInterpolated($"select * from \"CustomerPurchases\"({from.Date}, {to.Date})").ToListAsync());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [ProducesResponseType(typeof(IList<OrdersInProgress>), statusCode: 200)]
        public async Task<IActionResult> OrdersInProgress([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                return Ok(await _context.OrdersInProgress.FromSqlInterpolated($"select * from \"OrdersInProgress\"({from.Date}, {to.Date})").ToListAsync());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
