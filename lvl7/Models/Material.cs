using System;
using System.Collections.Generic;

namespace lvl7.Models
{
    public partial class Material
    {
        public Material()
        {
            MaterialHasProducts = new HashSet<MaterialHasProduct>();
        }

        public int IdMaterial { get; set; }
        public string MaterialName { get; set; } = null!;

        public virtual ICollection<MaterialHasProduct> MaterialHasProducts { get; set; }
    }
}
