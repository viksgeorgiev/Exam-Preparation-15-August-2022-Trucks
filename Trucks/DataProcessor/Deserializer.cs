using Newtonsoft.Json;
using System.Text;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ImportDto;
using Trucks.Utilities;

namespace Trucks.DataProcessor
{
    using Data;
    using System.ComponentModel.DataAnnotations;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            ImportDespatcherDto[]? importDespatcherDtos =
                XmlHelper.Deserialize<ImportDespatcherDto[]>(xmlString, "Despatchers");

            if (importDespatcherDtos != null && importDespatcherDtos.Length > 0)
            {
                //ICollection<Despatcher> despatchersToAdd = new List<Despatcher>();

                foreach (ImportDespatcherDto despatcherImport in importDespatcherDtos)
                {
                    if (!IsValid(despatcherImport))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (String.IsNullOrEmpty(despatcherImport.Position) || String.IsNullOrWhiteSpace(despatcherImport.Position))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Despatcher despatcher = new Despatcher()
                    {
                        Name = despatcherImport.Name,
                        Position = despatcherImport.Position,
                    };

                    foreach (ImportTruckDto truckImport in despatcherImport.Trucks)
                    {
                        if (!IsValid(truckImport))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        Truck truck = new Truck()
                        {
                            RegistrationNumber = truckImport.RegistrationNumber,
                            VinNumber = truckImport.VinNumber,
                            TankCapacity = truckImport.TankCapacity,
                            CargoCapacity = truckImport.CargoCapacity,
                            CategoryType = (CategoryType)truckImport.CategoryType,
                            MakeType = (MakeType)truckImport.MakeType,
                            Despatcher = despatcher,
                        };

                        context.Trucks.Add(truck);
                    }

                    context.Despatchers.Add(despatcher);
                    sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, despatcher.Name,
                        despatcher.Trucks.Count));
                }

                context.SaveChanges();
            }

            return sb.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportClientsJson[]? importClientsDto
                = JsonConvert.DeserializeObject<ImportClientsJson[]>(jsonString);

            if (importClientsDto != null && importClientsDto.Length > 0)
            {
                ICollection<Client> clientsToAdd = new List<Client>();

                foreach (ImportClientsJson importClients in importClientsDto)
                {
                    if (!IsValid(importClients))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (importClients.Type == "usual")
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Client client = new Client()
                    {
                        Name = importClients.Name,
                        Nationality = importClients.Nationality,
                        Type = importClients.Type,
                    };

                    int[] validTrucks = context.Trucks.Select(t => t.Id).ToArray();

                    foreach (int importTrucks in importClients.Trucks.Distinct())
                    {
                        if (!validTrucks.Contains(importTrucks))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        ClientTruck clientTruck = new ClientTruck()
                        {
                            Client = client,
                            TruckId = importTrucks,
                        };

                        context.ClientsTrucks.Add(clientTruck);
                    }

                    context.Clients.Add(client);
                    sb.AppendLine(string.Format(SuccessfullyImportedClient, client.Name, client.ClientsTrucks.Count));
                }
                context.SaveChanges();
            }
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}