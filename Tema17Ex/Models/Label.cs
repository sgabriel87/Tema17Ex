using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema17Ex.Models
{
    public class Label
    {
        public int Id { get; set; }
        public Guid BarCode { get; set; }
        public double Price { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
