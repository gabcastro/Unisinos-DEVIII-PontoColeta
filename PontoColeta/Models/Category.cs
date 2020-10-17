using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PontoColeta.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Coordinate> Coordinates { get; set; }
    }
}