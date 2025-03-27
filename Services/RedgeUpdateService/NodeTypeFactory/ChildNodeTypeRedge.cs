using KanUpdater.Services.RedgeUpdateService.Enum;

namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public class ChildNodeTypeRedge
    {
        public string TranslationTitle { get; set; } = "";
        public string TranslationDescription { get; set; } = "";
        public string KlhCode { get; set; } = "kLHCode";
        public string Category { get; set; } = "title";
        public string Genres { get; set; } = "programGenre";
        public string Tags { get; set; } = "generalTags";
        public string Platform { get; set; } = "";
        public RedgeContentType Type { get; set; }
    }
}
