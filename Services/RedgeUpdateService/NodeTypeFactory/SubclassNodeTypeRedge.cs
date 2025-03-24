namespace KanUpdater.Services.RedgeUpdateService.NodeTypeFactory
{
    public class SubclassNodeTypeRedge : ChildNodeTypeRedge
    {
        public SubclassNodeTypeRedge()
        {
            TranslationTitle = "title";
            Type = Enum.RedgeContentType.LIVE;
        }
    }
}
