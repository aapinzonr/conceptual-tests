using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CodeFirstDemo.Models
{
    public class DemoBuyItem
    {
        public int Id { get; set; }
        public DemoProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}
