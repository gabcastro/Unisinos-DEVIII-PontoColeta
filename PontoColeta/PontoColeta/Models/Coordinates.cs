using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PontoColeta.Models
{
    [Table("Coordinates")]
    public class Coordinates
    {
        [Key]
        [Display(Name = "IdCoordinate")]
        [Column("IdCoordinate")]
        public int IdCoordinates { get; set; }

        [Display(Name = "Latitude")]
        [Column("Latitude")]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Column("Longitude")]
        public string Longitude { get; set; }

        [Display(Name = "NomeLugar")]
        [Column("NomeLugar")]
        [AllowNull]
        public string NomeLugar { get; set; }

        [ForeignKey("Items")]
        public int IdRefItem { get; set; }
        public Items Items { get; set; }

    }
}
