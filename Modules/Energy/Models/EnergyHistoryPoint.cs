using System;

namespace HomeAppLBO.Modules.Energy.Models
{
    public sealed class EnergyHistoryPoint
    {
        public DateTime Date { get; set; }
        public double ValueKwh { get; set; }
    }
}