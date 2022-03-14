using Data;
using Data.Entities.Sale;
using Data.Repositories.Sale.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly SimpleDeliveryContext _context;
        private readonly ICustomersRepository _customers;

        public CustomersController(SimpleDeliveryContext context, ICustomersRepository customers)
        {
            _context = context;
            _customers = customers;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string description = null)
        {
            try
            {
                var customers = await _customers.GetAll();
                return Ok(customers);
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
                var product = await _customers.GetByKey(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
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
                await _customers.DeleteByKey(id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            try
            {
                if (id != customer.Id)
                    return BadRequest("Entity mismatch. Id provided on url is different of Id provided on body");

                if (!TryValidateModel(customer))
                    return BadRequest("Invalid data provided");

                await _customers.Update(customer);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            try
            {
                if (!TryValidateModel(customer))
                    return BadRequest("Invalid data provided");

                await _customers.Add(customer);
                await _context.SaveChangesAsync();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
