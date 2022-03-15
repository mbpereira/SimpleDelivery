using Core.Orders.Shipment.Contracts;
using Core.Orders.Shipment.Models;
using Data.Repositories.Store.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class ShipmentsController : BaseController
    {
        private readonly IShipmentCalculator _shipmentCalculator;
        private readonly IStoresRepository _stores;

        public ShipmentsController(IStoresRepository stores, IShipmentCalculator shipmentCalculator)
        {
            _stores = stores;
            _shipmentCalculator = shipmentCalculator;
        }

        [ProducesResponseType(typeof(ShipmentInfo), statusCode: 200)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string postalCode)
        {
            try
            {
                var store = await _stores.GetByKey(key: 1);
                var shipment = await _shipmentCalculator.Calculate(store.PostalCode, postalCode);

                return Ok(shipment);
            }
            catch(Exception ex)
            {
                return Error(ex);
            }
        }
    }
}