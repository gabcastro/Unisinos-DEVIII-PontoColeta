namespace PontoColeta.ViewModels.CoordinateViewModels
{
    public class ListCoordinateViewModel 
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string NameOfPlace { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}