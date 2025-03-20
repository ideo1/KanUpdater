using KanUpdater.Models.Metadata;
using KanUpdater.Services.RedgeUpdateService;
using Microsoft.AspNetCore.Mvc;

namespace KanUpdater.Controllers
{
    [Route("api/metadata")]
    public class MetadataController : ControllerBase
    {
        private readonly IRedgeUpdateService _redgeUpdateService;
        public MetadataController(IRedgeUpdateService redgeUpdateService)
        {
            _redgeUpdateService = redgeUpdateService;
        }
        /// <summary>
        /// Return metadata for specific Umbraco node
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Umbraco node metadata</returns>
        [HttpGet]
        [Route("getById")]      
        public IActionResult GetMetadataByContentId([FromQuery] MetadataRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _redgeUpdateService.GetRedgeUpdateModel(request.Id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

