using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfUserDAL : EfEntityRepositoryBase<User, NorthwindContext>, IUserDAL
    {
        public List<UserRoleItem> GetUserRoles(User user)
        {
            using (var context = new NorthwindContext())
            {
                var result = (from r in context.Roles
                              join ur in context.UserRoles on
                              r.Id equals ur.RoleId
                              where ur.UserId == user.Id
                              select new UserRoleItem { RoleName = r.Name }).ToList();

                return result;
            }
        }
    }
}
