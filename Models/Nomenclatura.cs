using System;
using System.Collections.Generic;

namespace ShopDbLib.Models
{
    public partial class Nomenclatura
    {
        public Nomenclatura()
        {
            Image = new HashSet<Image>();
           NomenclaturaComponents  = new HashSet<NomenclaturaComponents>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<NomenclaturaComponents> NomenclaturaComponents { get; set; }
    }
}
