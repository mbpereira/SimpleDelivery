using Data;
using Data.Repositories.Sale.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IActionResult> Sales([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                return Ok(await _orders.GetByInterval(from, to));
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }

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
