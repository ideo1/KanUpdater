using KanUpdater.Services.RedgeUpdateService.Models;

namespace KanUpdater.Services.RedgeSender
{
    public interface IRedgeSender
    {
        Task SendDataAsync(IEnumerable<RedgeUpdateRequestModel> items);
    }
}
