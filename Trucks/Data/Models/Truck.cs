﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Data.Models.Enums;

namespace Trucks.Data.Models
{
    public class Truck
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{2}\d{4}[A-Z]{2}")]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "CHAR(17)")]
        public string VinNumber { get; set; } = null!;

        [Range(950, 1420)]
        public int TankCapacity { get; set; }

        [Range(5000, 29000)]
        public int CargoCapacity { get; set; }

        [Required]
        [Range(0, 3)]
        public CategoryType CategoryType { get; set; }

        [Required]
        [Range(0, 4)]
        public MakeType MakeType { get; set; }

        [Required]
        [ForeignKey(nameof(Despatcher))]
        public int DespatcherId  { get; set; }
        public virtual Despatcher Despatcher { get; set; } = null!;


        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; } 
            = new HashSet<ClientTruck>();
    }
}
