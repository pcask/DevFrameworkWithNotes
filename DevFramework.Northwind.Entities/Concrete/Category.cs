using DevFramework.Core.Entities;
using System.Collections.Generic;

namespace DevFramework.Northwind.Entities.Concrete
{
    public class Category : IEntity
    {
        public virtual int CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual string Description { get; set; }

        // NHibernate kendi collection type'ına implement edebilmesi için concrete List yerine IList collection interface'i kullanılmalıdır.
        public virtual IList<Product> Products { get; set; }
    }
}
