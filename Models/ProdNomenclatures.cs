using System;
using System.Collections.Generic;

namespace DbClassLib.Models
{
    public partial class ProdNomenclatures
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int NomenclatureId { get; set; }

        public virtual Nomenclature Nomenclature { get; set; }
        public virtual Product Product { get; set; }
    }
}
