using KanUpdater.Services.RedgeUpdateService.Models;

namespace KanUpdater.Services.RedgeUpdateService
{
    public interface IRedgeUpdateService
    {
        RedgeUpdateRequestModel? GetContentBasedRedgeUpdateModel(int contentId);
        RedgeUpdateRequestModel? GetCachetBasedRedgeUpdateModel(int contentId);
        IEnumerable<RedgeUpdateRequestModel?> GetContentBasedRedgeUpdateModels(IEnumerable<int> ids);
        IEnumerable<RedgeUpdateRequestModel?> GetCacheBasedRedgeUpdateModels(IEnumerable<int> ids);
    }
}
