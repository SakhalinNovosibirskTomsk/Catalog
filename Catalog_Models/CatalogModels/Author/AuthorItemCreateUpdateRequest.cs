using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.Author
{
    public class AuthorItemCreateUpdateRequest
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(200)]
        public string? MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Является ли зарубежным автором
        /// </summary>
        public bool IsForeign { get; set; } = false;

    }
}
