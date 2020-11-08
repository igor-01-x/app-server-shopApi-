using System;
using System.Collections.Generic;

namespace DbClassLib.Models
{
    public partial class Katalog
    {
        public Katalog()
        {
            Model = new HashSet<Model>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Model> Model { get; set; }
    }
}
