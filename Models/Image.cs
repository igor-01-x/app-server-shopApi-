using System;
using System.Collections.Generic;

namespace ShopDbLib.Models
{
    public partial class Image
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
