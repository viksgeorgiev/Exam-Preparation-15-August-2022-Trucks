﻿namespace Trucks.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Nationality { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public virtual ICollection<ClientTruck> ClientsTrucks  { get; set; } 
            = new HashSet<ClientTruck>();
    }
}
