using KanUpdater.Services.RedgeUpdateService.Models;

namespace KanUpdater.Services.RedgeUpdateService
{
    public interface IRedgeUpdateService
    {
        RedgeUpdateRequestModel? GetRedgeUpdateModel(int contentId);
    }
}
