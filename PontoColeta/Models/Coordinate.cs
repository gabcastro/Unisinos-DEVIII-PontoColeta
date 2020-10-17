using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoColeta.Models
{
    public class Coordinate
    {
        public int Id { get; set; }

        /* 
            Coordinates taking in consideration values from the state where I live
            i.e. -30.045787, -51.176764
            Longitude will be between -27.[something] and -33.[something]
            Latitude will be between -50.[something] and -57.[something]
            Problems with coordinates positives or shorter, could happen 
        
            -> Longitude: vertical
            -> Latitude: horizontal
        */
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        /* In the API of Google Maps, it's possible use a name of a place instead coordinates.
        So, something we can save the name without care about geolocation */
        public string NameOfPlace { get; set; }
        public int IdCategory { get; set; }
        public Category Category { get; set; }
    }
}