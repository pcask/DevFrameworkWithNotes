using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings
{
    public class RoleMAP : EntityTypeConfiguration<Role>
    {
        public RoleMAP()
        {
            ToTable("Roles");
            HasKey(r => r.Id);

            Property(x => x.Name).HasColumnName("Name");
        }
    }
}
