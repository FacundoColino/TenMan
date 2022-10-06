using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TenMan.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboSpecialties();
        IEnumerable<SelectListItem> GetComboTenants();

        IEnumerable<SelectListItem> GetComboUnits(int id);
    }
}