using Core.Orders.Contracts;
using Data;
using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly SimpleDeliveryContext _context;
        private readonly IOrdersHandler _ordersHandler;
        private readonly IOrdersRepository _orders;

        public OrdersController(IOrdersHandler ordersHandler, SimpleDeliveryContext context, IOrdersRepository orders)
        {
            _ordersHandler = ordersHandler;
            _context = context;
            _orders = orders;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _orders.GetAll());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var order = await _orders.GetByKey(id);
                
                if (order == null)
                    return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
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

        [HttpPatch("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _ordersHandler.Cancel(id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _ordersHandler.Delete(id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {
            try
            {
                if (id != order.Id)
                    return BadRequest("Entity mismatch. Id provided on url is different of Id provided on body");
                if (!TryValidateModel(order))
                    return BadRequest("Invalid data provided");

                await _ordersHandler.Update(order);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
