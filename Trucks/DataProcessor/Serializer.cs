using Newtonsoft.Json;
using Trucks.DataProcessor.ExportDto;
using Trucks.Utilities;

namespace Trucks.DataProcessor
{
    using Data;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            var despatchers = context
                .Despatchers
                .Where(d => d.Trucks.Any())
                .Select(d => new ExportDespatchersDto()
                {
                    DespatcherName = d.Name,
                    TrucksCount = d.Trucks.Count.ToString(),
                    Trucks = d.Trucks.Select(t => new ExportTrucksDto()
                    {
                        RegistrationNumber = t.RegistrationNumber,
                        Make = t.MakeType.ToString(),
                    })
                    .OrderBy(t => t.RegistrationNumber)
                    .ToArray()
                })
                .OrderByDescending(d => d.TrucksCount)
                .ThenBy(d => d.DespatcherName)
                .ToArray();

            string result = XmlHelper.Serialize(despatchers, "Despatchers");
            return result;
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clients = context
                .Clients
                .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
                .ToArray()
                .Select(c => new
                {
                    c.Name,
                    Trucks = c.ClientsTrucks
                        .Where(ct => ct.Truck.TankCapacity >= capacity)
                        .ToArray()
                        .OrderBy(ct => ct.Truck.MakeType.ToString())
                        .ThenByDescending(ct => ct.Truck.CargoCapacity)
                        .Select(ct => new
                        {
                            TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                            VinNumber = ct.Truck.VinNumber,
                            TankCapacity = ct.Truck.TankCapacity,
                            CargoCapacity = ct.Truck.CargoCapacity,
                            CategoryType = ct.Truck.CategoryType.ToString(),
                            MakeType = ct.Truck.MakeType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(c => c.Trucks.Length)
                .ThenBy(c => c.Name)
                .Take(10)
                .ToArray();

            string result = JsonConvert.SerializeObject(clients,Formatting.Indented);

            return result;
        }
    }
}
