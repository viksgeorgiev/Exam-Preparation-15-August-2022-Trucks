using System.Xml.Serialization;

namespace Trucks.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    [XmlType("Truck")]
    public class ImportTruckDto
    {
        [XmlElement(nameof(RegistrationNumber))]
        [Required]
        [RegularExpression(@"[A-Z]{2}\d{4}[A-Z]{2}")]
        public string RegistrationNumber { get; set; } = null!;

        [XmlElement(nameof(VinNumber))]
        [Required]
        [MaxLength(17)]
        public string VinNumber { get; set; } = null!;

        [XmlElement(nameof(TankCapacity))]
        [Range(950, 1420)]
        public int TankCapacity { get; set; }

        [XmlElement(nameof(CargoCapacity))]
        [Range(5000, 29000)]
        public int CargoCapacity { get; set; }

        [XmlElement(nameof(CategoryType))]
        [Required]
        [Range(0, 3)]
        public int CategoryType { get; set; }

        [XmlElement(nameof(MakeType))]
        [Required]
        [Range(0, 4)]
        public int MakeType { get; set; }
    }
}