using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")]
    public class ExportDespatchersDto
    {
        [XmlElement(nameof(DespatcherName))]
        public string DespatcherName { get; set; } = null!;

        [XmlAttribute(nameof(TrucksCount))]
        public string TrucksCount { get; set; } = null!;

        [XmlArray(nameof(Trucks))]
        public ExportTrucksDto[] Trucks { get; set; } = null!;
    }
}
