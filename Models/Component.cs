using System;
using System.Collections.Generic;

namespace ShopDbLib.Models
{
    public partial class Component
    {
        public Component()
        {
            NomenclaturaComponents = new HashSet<NomenclaturaComponents>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float? Price { get; set; }

        public virtual ICollection<NomenclaturaComponents> NomenclaturaComponents { get; set; }
    }
}
