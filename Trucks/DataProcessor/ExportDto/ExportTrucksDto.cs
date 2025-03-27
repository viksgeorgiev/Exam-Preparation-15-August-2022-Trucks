﻿using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Truck")]
    public class ExportTrucksDto
    {
        [XmlElement(nameof(RegistrationNumber))]
        public string RegistrationNumber { get; set; } = null!;

        [XmlElement(nameof(Make))]
        public string Make { get; set; } = null!;
    }
}