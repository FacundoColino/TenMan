using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TenMan.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboFields();
        IEnumerable<SelectListItem> GetComboSpecialties();
        IEnumerable<SelectListItem> GetComboTenants();
        IEnumerable<SelectListItem> GetComboWorkers();
        IEnumerable<SelectListItem> GetComboUnits(int id);
        IEnumerable<SelectListItem> GetComboStatusTypes();
    }
}