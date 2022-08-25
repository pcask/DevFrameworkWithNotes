using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserRoleMAP : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMAP()
        {
            ToTable("UserRoles");
            HasKey(u => u.Id);

            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.RoleId).HasColumnName("RoleId");
        }
    }
}
