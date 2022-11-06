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
        public IEnumerable<SelectListItem> GetComboTenants()
        {
            var list = _context.Tenants.Select(ten => new SelectListItem
            {
                Text = ten.User.FullName,
                Value = ten.Id.ToString()
                //Value = $"{ ten.Id }" // Convierte a string
            }).OrderBy(ten => ten.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione el locatario",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboWorkers()
        {
            var list = _context.Workers.Select(w => new SelectListItem
            {
                Text = w.User.FullName,
                Value = w.Id.ToString()
                //Value = $"{ ten.Id }" // Convierte a string
            }).OrderBy(w => w.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione el especialista",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboFields()
        {
            var list = _context.Fields.Select(f => new SelectListItem
            {
                Text = f.Description,
                Value = f.Id.ToString()
            }).OrderBy(f => f.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione el rubro",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboUnits(int id)
        {
            _context.Units.Where(unit => unit.Tenant.Id == id);
            var query = from u in _context.Units
                        where u.Tenant.Id == id
                        select new SelectListItem
                        {
                            Text = u.Number.ToString() + "(" + u.Floor + u.Apartment + ")",
                            Value = u.Id.ToString()
                        };

            var list = query.ToList();

            //var list = _context.Units.Select(unit => new SelectListItem
            //{
            //    Text = unit.Number.ToString() + "(" + unit.Floor + unit.Apartment + ")",
            //    Value = unit.Id.ToString()
            //    //Value = $"{ ten.Id }" // Convierte a string
            //}).OrderBy(unit => unit.Text)
            //    .ToList(); FUNCIONA

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione la unidad",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboStatusTypes()
        {
            var list = _context.StatusTypes.Select(st => new SelectListItem
            {
                Text = st.Description,
                Value = $"{ st.Id }" // Convierte a string
            }).OrderBy(st => st.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione el estado",
                Value = "0"
            });

            return list;
        }
    }
}
