using System.Collections.Generic;

namespace PontoColeta.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Coordinate> Coordinates { get; set; }
    }
}