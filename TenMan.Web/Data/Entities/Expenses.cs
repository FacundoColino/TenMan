using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Models;

namespace TenMan.Web.Data.Entities
{
    public class Expenses
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Mes")]
        [Range(1, 12, ErrorMessage = "Debe ingresar un mes.")]
        public int Month { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Año")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un año.")]
        public int Year { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha de Generación")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLocal => Date.ToLocalTime();

        public IEnumerable<ExpensesCost> ExpensesCosts { get; set; }
        //public IEnumerable<string> Notes { get; set; }
        public Committee Committee { get; set; }
        public int CommitteeId { get; set; }
        public IEnumerable<UnitDescriptionLine> UnitDescriptionLines { get; set; }
        public IEnumerable<Field> Fields { get; set; }
        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (ExpensesCost cost in ExpensesCosts)
            {
                total += cost.Amount;
            }
            return total;
        }
        public bool CostWithField(string fieldDescription)
        {
            foreach (ExpensesCost cost in ExpensesCosts)
            {
                if (cost.Field.Description == fieldDescription)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
