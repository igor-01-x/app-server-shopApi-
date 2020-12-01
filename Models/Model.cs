using System;
using System.Collections.Generic;

namespace ShopDbLib.Models
{
    public partial class Model
    {
        public Model()
        {
            Nomenclatura = new HashSet<Nomenclatura>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float? Price { get; set; }
        public int KatalogId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual Katalog Katalog { get; set; }  // навигационное свойство
        public virtual ICollection<Nomenclatura> Nomenclatura { get; set; }
    }
}
