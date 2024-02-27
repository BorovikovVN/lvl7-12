using System;
using System.Collections.Generic;

namespace lvl7.Models
{
    public partial class MaterialHasProduct
    {
        public int ProductIdProduct { get; set; }
        public int MaterialIdMaterial { get; set; }
        public string? Amount { get; set; }

        public virtual Material MaterialIdMaterialNavigation { get; set; } = null!;
        public virtual Product ProductIdProductNavigation { get; set; } = null!;
    }
}
