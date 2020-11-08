using System;
using System.Collections.Generic;

namespace DbClassLib.Models
{
    public partial class Nomenclature
    {
        public Nomenclature()
        {
            ProdNomenclatures = new HashSet<ProdNomenclatures>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float? Price { get; set; }

        public virtual ICollection<ProdNomenclatures> ProdNomenclatures { get; set; }
    }
}
