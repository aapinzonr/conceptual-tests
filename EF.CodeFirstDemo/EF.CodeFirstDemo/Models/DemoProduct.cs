using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CodeFirstDemo.Models
{
    public class DemoProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int InInventory { get; set; }
        public bool Enabled { get; set; } = true;
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
    }
}
