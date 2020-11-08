using System;
using System.Collections.Generic;

namespace DbClassLib.Models
{
    public partial class Состав
    {
        public Состав()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NomenclatureId { get; set; }

        public virtual Nomenclature Nomenclature { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
