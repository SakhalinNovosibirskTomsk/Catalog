using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.Book
{
    /// <summary>
    /// Справочник книг
    /// </summary>
    public class BookItemCreateUpdateRequest
    {
        /// <summary>
        /// Наименование книги
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        [DisplayName("Наименование книги")]
        public string Name { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        [MaxLength(17)]
        [DisplayName("ISBN")]
        public string? ISBN { get; set; }

        /// <summary>
        /// Наименование издателя
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        public string PublisherName { get; set; }

        /// <summary>
        /// Дата издания
        /// </summary>        
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Авторы книги
        /// </summary>
        public List<BookToAuthorItemCreateUpdateRequest> BookAuthors { get; set; }


    }
}
