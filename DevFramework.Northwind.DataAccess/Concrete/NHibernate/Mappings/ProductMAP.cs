using DevFramework.Northwind.Entities.Concrete;
using FluentNHibernate.Mapping;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Mappings
{
    public class ProductMAP : ClassMap<Product>
    {
        public ProductMAP()
        {
            Table(@"Products");
            Schema("dbo");
            LazyLoad();
            Id(x => x.ProductId).Column("ProductID");


            Map(x => x.CategoryId); // burada .Column("CategoryID") eklemek hataya sebeb olucaktır. References method'da belirtilmesi yeterli olacaktır.
            Map(x => x.ProductName);
            Map(x => x.UnitPrice);
            Map(x => x.UnitsInStock);
            Map(x => x.QuantityPerUnit);

            References(x => x.Category, "CategoryID");

        }
    }
}
