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

        /// <summary>
        /// Retorna o valor de frete e os detalhes da origem x destino. 
        /// Por padrão, o cálculo é realizado levando em consideração a loja padrão (Código 1),
        /// cuja sede está localizada no Rio de Janeiro.
        /// As informações dessa loja podem ser consultadas através de uma requisição GET, no endereço
        /// api/Stores/1. Ex.: GET api/Stores/1
        /// </summary>
        /// <param name="postalCode">Cep destino</param>
        /// <returns></returns>
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