namespace HomeAppLBO.Modules.Security.Models
{
    public sealed class SecurityCamera
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string StatusText { get; set; } = "";
        public string PreviewText { get; set; } = "Aperçu caméra";
        public bool IsOnline { get; set; }
    }
}