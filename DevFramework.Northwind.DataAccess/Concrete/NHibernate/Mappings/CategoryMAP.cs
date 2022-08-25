using DevFramework.Northwind.Entities.Concrete;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Mappings
{
    public class CategoryMAP : ClassMap<Category>
    {
        public CategoryMAP()
        {
            Table(@"Categories");
            Schema("dbo");
            LazyLoad();
            Id(x => x.CategoryId).Column("CategoryID");

            Map(x => x.CategoryName);
            Map(x => x.Description);

            // Bire-Çok ilişki
            HasMany(c => c.Products);
        }
    }
}
