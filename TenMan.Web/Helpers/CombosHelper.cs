using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data;

namespace TenMan.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetComboSpecialties()
        {
            var list = _context.Specialties.Select(sp => new SelectListItem
            {
                Text = sp.Name,
                Value = $"{ sp.Id }" // Convierte a string
            }).OrderBy(sp => sp.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione la especialidad del arreglo",
                Value = "0"
            });

            return list;
        }
    }
}
