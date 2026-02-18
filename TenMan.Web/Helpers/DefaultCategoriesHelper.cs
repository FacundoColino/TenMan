using System.Collections.Generic;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Helpers
{
    public class DefaultCategoriesHelper : IDefaultCategoriesHelper
    {
        public List<Category> GetCategoriesDefault()
        {
            return new List<Category>
                {
                new Category {  Letra = "A",
                    Description = "Gastos generales comúnes a todas las unidades"
                }
            };
        }
    }
}
