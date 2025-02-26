using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект запроса обновления статуса экземпляра книги
    /// </summary>
    public class BookInstanceUpdateStateRequest
    {

        /// <summary>
        /// ИД статуса состояния книги
        /// </summary>
        [Required]
        [DisplayName("ИД статуса состояния книги")]
        public int StateId { get; set; }

        /// <summary>
        /// ИД комментария к статусу состояния книги
        /// </summary>
        [DisplayName("ИД комментария к статусу состояния книги")]
        public int? FactCommentId { get; set; } = null;

        /// <summary>
        /// Текст комментария к статусу состояния книги
        /// </summary>
        [DisplayName("Текст комментария к статусу состояния книги")]
        public string? FactCommentText { get; set; } = String.Empty;

    }
}
