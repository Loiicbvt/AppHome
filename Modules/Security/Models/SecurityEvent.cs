namespace HomeAppLBO.Modules.Security.Models
{
    public sealed class SecurityEvent
    {
        public string Id { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime Time { get; set; }

        public string Icon { get; set; } = "🔔";
    }
}