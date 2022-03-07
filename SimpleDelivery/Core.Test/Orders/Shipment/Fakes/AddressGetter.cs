using Core.Integration.Address.Contracts;
using Core.Orders.Shipment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Test.Orders.Shipment.Fakes
{
    public class AddressGetter : IAddressGetter
    {
        private readonly List<AddressInfo> _addresses;

        public AddressGetter()
        {
            _addresses = GetAddresses();
        }

        public Task<AddressInfo> GetAddress(string postalCode)
        {
            postalCode = Format(postalCode);
            return Task.Run(() => _addresses.FirstOrDefault(a => Format(a.PostalCode).Equals(postalCode)));
        }

        private string Format(string postalCode)
        {
            return postalCode.Replace("-", "");
        }

        private List<AddressInfo> GetAddresses()
        {
            return new List<AddressInfo>()
            {
                new AddressInfo()
                {
                    AddressStreet = "Rua Chupim",
                    City = "Cuiabá",
                    PostalCode = "78058-160",
                    Uf = "MT"
                },
                new AddressInfo()
                {
                    AddressStreet = "Av. Tancredo Neves",
                    City = "Cuiabá",
                    PostalCode = "78065-230",
                    Uf = "MT"
                },
                new AddressInfo()
                {
                    AddressStreet = "Av. Tancredo Neves",
                    City = "Cuiabá",
                    PostalCode = "78065-230",
                    Uf = "MT"
                },
                new AddressInfo()
                {
                    AddressStreet = "Avenida das Embaúbas",
                    City = "Sinop",
                    PostalCode = "78550-970",
                    Uf = "MT"
                },
                new AddressInfo()
                {
                    AddressStreet = "Rua do Passeio",
                    City = "Rio de Janeiro",
                    PostalCode = "20021-290",
                    Uf = "RJ"
                },
                new AddressInfo()
                {
                    AddressStreet = "Rua do Catete",
                    City = "Rio de Janeiro",
                    PostalCode = "22220-001",
                    Uf = "RJ"
                },
                new AddressInfo()
                {
                    AddressStreet = "Avenida Getúlio Vargas 19 Loja A",
                    City = "Arraial do Cabo",
                    PostalCode = "28930-970",
                    Uf = "RJ"
                }
            };
        }
    }
}
