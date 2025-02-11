using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.Publisher
{
    public class PublisherItemCreateUpdateRequest
    {
        /// <summary>
        /// Наименование издателя
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        public string Name { get; set; }

    }
}
