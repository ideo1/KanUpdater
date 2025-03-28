using Umbraco.Cms.Core.Models;

namespace KanUpdater.Services.RedgeUpdateService.Models
{
    public class ContentMapModel
    {
        public required IContent AssignedContent { get; set; }
        public  IContent AssignedProgram { get; set; }
        public  IContent AssignedSubclass { get; set; }
        public  IContent Root { get; set; }
    }
}
