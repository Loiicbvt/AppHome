namespace HomeAppLBO.Modules.Energy.Models
{
    public sealed class EnergyDevice
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string StatusText { get; set; } = "";
        public string DetailText { get; set; } = "";
        public string Icon { get; set; } = "⚡";
    }
}