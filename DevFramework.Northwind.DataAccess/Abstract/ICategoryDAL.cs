using DevFramework.Core.DataAccess;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Abstract
{
    public interface ICategoryDAL : IEntityRepository<Category>
    {
        // Burada sadece Category'e özgü method imzaları yer alacak. 
    }
}
