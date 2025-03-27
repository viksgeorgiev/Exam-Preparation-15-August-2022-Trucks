using System.Xml.Serialization;

namespace Trucks.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    [XmlType("Despatcher")]
    public class ImportDespatcherDto
    {
        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(Position))]
        public string? Position { get; set; }

        [XmlArray(nameof(Trucks))] 
        public ImportTruckDto[] Trucks { get; set; } = null!;
    }
}
