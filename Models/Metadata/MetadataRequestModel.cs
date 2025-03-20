using System.ComponentModel.DataAnnotations;

namespace KanUpdater.Models.Metadata
{
    public class MetadataRequestModel
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Id should be positive.")]
        public int Id { get; set; }
    }
}
