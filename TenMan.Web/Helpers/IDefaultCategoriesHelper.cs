using System.Collections.Generic;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Helpers
{
    public interface IDefaultCategoriesHelper
    {
        List<Category> GetCategoriesDefault();
    }
}
