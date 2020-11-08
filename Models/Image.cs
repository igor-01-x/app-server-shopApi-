using System;
using System.Collections.Generic;

namespace DbClassLib.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
