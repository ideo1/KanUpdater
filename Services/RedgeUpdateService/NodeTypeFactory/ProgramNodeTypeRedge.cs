namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public class ProgramNodeTypeRedge : ChildNodeTypeRedge
    {
        public ProgramNodeTypeRedge()
        {
            TranslationTitle = "title";
            Type = Enum.RedgeContentType.SERIES;
            TranslationDescription = "programContent";
            Platform = "platform";
        }
    }
}
