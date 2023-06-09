using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EF.CodeFirstDemo.Models
{
    public class DemoBuy
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //public DocumentType ClientDocumentType { get; set; }
        public string ClientDocument { get; set; }
        public string ClientName { get; set; }
        public IList<DemoBuyItem> Products { get; set; }
    }
}
