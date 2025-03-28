namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public class SeasonNodeTypeRedge : ChildNodeTypeRedge
    {
        public SeasonNodeTypeRedge()
        {
            TranslationTitle = "title";            
            Type = Enum.RedgeContentType.SEASON;
            Number = "seasonNumber";
        }
    }
}
