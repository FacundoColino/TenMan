using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class UnitDescriptionLine
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }

        //Solo agrego aqui los campos que van a cambiar respecto al Checking Account que ya está llevando un balance
        // Ejemplo: El pago anterior ya está contenido dentro de Checking Account y a la hora de calcular no cambia.
        public decimal Interest { get; set; }

        public decimal ExpA { get; set; }

        public decimal PendingBalance { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal YourPayment { get; set; }

        public decimal NewUnitTotal { get; set; }
        public IList<CategoryPercent> CategoriesPercents { get; set; }

        [NotMapped]
        public IList<AmountByPercent> AmountsByPercent { get; set; }
        public int ExpensesId { get; set; }

    }
}
