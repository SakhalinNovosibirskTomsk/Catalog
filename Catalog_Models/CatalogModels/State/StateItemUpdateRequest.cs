using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.State
{
    public class StateItemUpdateRequest
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public bool IsNeedComment { get; set; } = false;
    }
}
