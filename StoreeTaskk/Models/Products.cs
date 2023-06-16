using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreeTaskk.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Prices { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
    }
}
