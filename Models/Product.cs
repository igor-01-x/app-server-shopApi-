using System;
using System.Collections.Generic;

namespace DbClassLib.Models
{
    public partial class Product
    {
        public Product()
        {
            Image = new HashSet<Image>();
            ProdNomenclatures = new HashSet<ProdNomenclatures>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<ProdNomenclatures> ProdNomenclatures { get; set; }
    }
}
