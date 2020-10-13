using System.ComponentModel.DataAnnotations;

namespace PontoColeta.Models
{
    public class Coordinate
    {
        [Key]
        public int Id { get; set; }

        /* 
            Coordinates taking in consideration values from the state where I live
            i.e. -30.045787, -51.176764
            Logitude will be between -27.[something] and -33.[something]
            Latitude will be between -50.[something] and -57.[something]
            Problems with coordinates positives or shorter, could happen 
        
            -> Lontitude: vertical
            -> Latitude: horizontal
        */
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 10 caracteres")]
        [MaxLength(10, ErrorMessage = "Este campo deve conter entre 3 e 10 caracteres")]
        public string Latitude { get; set; }

        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 10 caracteres")]
        [MaxLength(10, ErrorMessage = "Este campo deve conter entre 3 e 10 caracteres")]
        public string Longitude { get; set; }

        /* In the API of Google Maps, it's possible use a name of a place instead coordinates.
        So, something we can save the name without care about geolocation */
        public string NameOfPlace { get; set; }

        public int IdCategory { get; set; }

        public Category Category { get; set; }
    }
}