using KanUpdater.Services.RedgeUpdateService.Models;

namespace KanUpdater.Services.RedgeSender
{
    public class RedgeSender : IRedgeSender
    {
        public Task SendDataAsync(IEnumerable<RedgeUpdateRequestModel> items)
        {
            throw new NotImplementedException();
        }
    }
}
