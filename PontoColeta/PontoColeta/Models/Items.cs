using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoColeta.Models
{
    [Table("Items")]
    public class Items
    {
        [Key]
        [Display(Name = "IdItem")]
        [Column("IdItem")]
        public int IdItem { get; set; }

        [Display(Name = "Item")]
        [Column("Item")]
        public String Item { get; set; }

        public ICollection<Coordinates> Coordinates { get; set; }
    }
}
