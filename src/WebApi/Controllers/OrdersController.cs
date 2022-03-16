using Core.Orders.Contracts;
using Data;
using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [ProducesResponseType(typeof(IList<Order>), statusCode: 200)]
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

        [ProducesResponseType(typeof(Order), statusCode: 200)]
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

        /// <summary>
        /// Cria um novo pedido.
        /// Obs.: 
        /// *Não é possível gerar um pedido com status Cancelado;
        /// *Apenas pedidos aprovados (Aprovado, Em Preparo, Entregue) movimentam estoque;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Order), statusCode: 200)]
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

        /// <summary>
        /// Marca um pedido como cancelado. 
        /// Obs.: Não é permitido modificar o status de pedidos já finalizados (Entregues, Cancelados)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(statusCode: 204)]
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

        /// <summary>
        /// Marca um pedido como aprovado. 
        /// Obs.: Não é permitido modificar o status de pedidos já finalizados (Entregues, Cancelados)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(statusCode: 204)]
        [HttpPatch("{id:int}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await _ordersHandler.Approve(id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Marca um pedido como em preparo. 
        /// Obs.: Não é permitido modificar o status de pedidos já finalizados (Entregues, Cancelados)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(statusCode: 204)]
        [HttpPatch("{id:int}/prepare")]
        public async Task<IActionResult> Prepare(int id)
        {
            try
            {
                await _ordersHandler.Prepare(id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Marca um pedido como entregue. 
        /// Obs.: Não é permitido modificar o status de pedidos já finalizados (Entregues, Cancelados)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(statusCode: 204)]
        [HttpPatch("{id:int}/deliver")]
        public async Task<IActionResult> Deliver(int id)
        {
            try
            {
                await _ordersHandler.Deliver(id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// Apaga um pedido. 
        /// Obs.: Não é permitido modificar o status de pedidos já finalizados (Entregues, Cancelados)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(statusCode: 204)]
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

        /// <summary>
        /// Atualiza um pedido. 
        /// Obs.: 
        /// *Não é permitido modificar o status de pedidos já finalizados (Entregues, Cancelados);
        /// *Este endpoint não permite o cancelamento de um pedido. Para isso, utilize:
        /// PATCH api/Orders/{id}/cancel;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(statusCode: 204)]
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
