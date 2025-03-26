using KanUpdater.Services.RedgeUpdateService.Models;
using Newtonsoft.Json;

namespace KanUpdater.Services.RedgeSender
{
    public class RedgeSenderStub : IRedgeSender
    {
        private readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Stub", "Migration");
        public async Task SendDataAsync(IEnumerable<RedgeUpdateRequestModel> items)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Path.Combine(folderPath, $"test-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.json");

                var jsonData = JsonConvert.SerializeObject(items);
                await File.WriteAllTextAsync(fileName, jsonData);

                //return Ok(new { Message = "File written successfully.", FilePath = fileName });
            }
            catch (System.Exception ex)
            {
               // return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
            }


            //return Task.CompletedTask;
        }
    }
}
