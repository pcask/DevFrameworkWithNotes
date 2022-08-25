using DevFramework.Northwind.Entities.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings
{
    // Normalde EntityFramework entity'lerimizi tablolarımızla Conventional isimlendirme kurallarına uyulduğu sürece sorunsuzca eşleştirir.
    // Fakat eşleştirme işlemini kendimiz yönetmek isteyebiliriz. Örneğin eski bir database'de türkçe isimlendirmeler kullanılmış olabilir.
    public class ProductMAP : EntityTypeConfiguration<Product>
    {
        public ProductMAP()
        {
            ToTable(@"Products", @"dbo");
            HasKey(x => x.ProductId);

            Property(x => x.ProductId).HasColumnName("ProductID");
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired();
            Property(x => x.CategoryId).HasColumnName("CategoryID");
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock");
            Property(x => x.UnitPrice).HasColumnName("UnitPrice");

        }
    }
}
