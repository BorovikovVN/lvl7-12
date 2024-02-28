using System;
using System.Collections.Generic;

namespace lvl7.Models
{
    public partial class Product
    {
        public Product()
        {
            MaterialHasProducts = new HashSet<MaterialHasProduct>();
        }

        public int IdProduct { get; set; }
        public string NameProduct { get; set; } = null!;
        public int TypeProduct { get; set; }
        public int Price { get; set; }
        public string Article { get; set; } = null!;
        public byte[]? Photo { get; set; }

        public string Title => TypeProductNavigation.TypeName + " | " + NameProduct;

        public String ArticleTitle => "Артикул: " + Article;

        public String MaterialList => "Материалы: " + String.Join(", ", MaterialHasProducts.Select(pm => pm.MaterialIdMaterialNavigation.MaterialName));

        public virtual ProductType TypeProductNavigation { get; set; } = null!;
        public virtual ICollection<MaterialHasProduct> MaterialHasProducts { get; set; }
    }
}
