using System;
using System.Collections.Generic;

namespace ShopDbLib.Models
{
    public partial class NomenclaturaComponents
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ComponentId { get; set; }

        public virtual Component Component { get; set; }
        public virtual Nomenclatura Nomenclatura { get; set; }
    }
}
