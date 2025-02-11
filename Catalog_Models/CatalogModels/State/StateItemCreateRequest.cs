using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.State
{
    public class StateItemCreateRequest
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }


        public bool IsInitialState { get; set; } = false;
        public bool IsNeedComment { get; set; } = false;
        public bool IsArchive { get; set; }
    }
}
