using System;
using System.Collections.Generic;

namespace DbClassLib.Models
{
    public partial class Nomenclature
    {
        public Nomenclature()
        {
            Состав = new HashSet<Состав>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Состав> Состав { get; set; }
    }
}
