namespace HomeAppLBO.Modules.Organization.Models
{
    public sealed class FamilyEvent
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime StartDate { get; set; }
        public string Icon { get; set; } = "📅";
    }
}