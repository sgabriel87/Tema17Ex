using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema17Ex.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CUI { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
