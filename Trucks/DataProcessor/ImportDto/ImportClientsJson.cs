namespace Trucks.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class ImportClientsJson
    {
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

        [Required] 
        public int[] Trucks { get; set; } = null!;
    }
}
