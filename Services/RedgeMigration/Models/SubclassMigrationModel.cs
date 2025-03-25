namespace KanUpdater.Services.RedgeMigration.Models
{
    public class SubclassMigrationModel
    {
        public IEnumerable<SubclassMigrationItem> Subclasses { get; set; }
    }

    public class SubclassMigrationItem
    {
        public int UmbracoId { get; set; }
    }
}
