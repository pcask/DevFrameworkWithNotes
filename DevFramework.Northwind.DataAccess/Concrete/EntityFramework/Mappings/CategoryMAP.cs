using DevFramework.Northwind.Entities.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CategoryMAP : EntityTypeConfiguration<Category>
    {
        public CategoryMAP()
        {
            ToTable(@"Categories", @"dbo");
            HasKey(x => x.CategoryId);

            Property(x => x.CategoryId).HasColumnName("CategoryID");
            Property(x => x.CategoryName).HasColumnName("CategoryName");
            Property(x => x.Description).HasColumnName("Description");
        }
    }
}
