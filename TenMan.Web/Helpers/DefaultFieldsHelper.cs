using System.Collections.Generic;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Helpers
{
    public class DefaultFieldsHelper : IDefaultFieldsHelper
    {
        public List<Field> GetRubrosDefault()
        {
            return new List<Field>
                {
                new Field { Number = 1, Description = "DETALLE DE SUELDO Y CARGAS SOCIALES" },
                new Field { Number = 2, Description = "SERVICIOS PÚBLICOS" },
                new Field { Number = 3, Description = "ABONOS DE SERVICIOS" },
                new Field { Number = 4, Description = "MANTENIMIENTO DE PARTES COMÚNES" },
                new Field { Number = 5, Description = "REPARACIONES EN UNIDADES FUNCIONALES" },
                new Field { Number = 6, Description = "GASTOS BANCARIOS" },
                new Field { Number = 7, Description = "GASTOS DE LIMPIEZA" },
                new Field { Number = 8, Description = "GASTOS DE ADMINISTRACIÓN" },
                new Field { Number = 9, Description = "OTROS" }
                };
        }
    }
}
