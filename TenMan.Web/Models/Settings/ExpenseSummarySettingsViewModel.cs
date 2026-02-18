namespace TenMan.Web.Models.Settings
{
    public class ExpenseSummarySettingsViewModel
    {
        public int CommitteeId { get; set; }

        // Header
        public string HeaderAlignment { get; set; } // Left | Center | Right

        // Opciones de visualización
        public bool ShowCategoryTotals { get; set; }
        public bool ShowFieldTotals { get; set; }
        public bool ShowUnitDetail { get; set; }
        public bool ShowFixedCostsDetail { get; set; }

        // Agregar Color Picker para los titulos

        // Agregar mostrar comprobantes
    }
}
