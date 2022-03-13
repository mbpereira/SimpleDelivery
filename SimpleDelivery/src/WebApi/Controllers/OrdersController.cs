using Core.Orders.Contracts;
using Data;
using Data.Entities.Sale;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly SimpleDeliveryContext _context;
        private readonly IOrdersHandler _ordersHandler;

        public OrdersController(IOrdersHandler ordersHandler, SimpleDeliveryContext context)
        {
            _ordersHandler = ordersHandler;
            _context = context;
        }

        public async Task<IActionResult> Post([FromBody] Order order)
        {
            try
            {
                if (!TryValidateModel(order))
                    return BadRequest("Invalid data provided");

                await _ordersHandler.Create(order);
                await _context.SaveChangesAsync();
                return Ok(order);
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
