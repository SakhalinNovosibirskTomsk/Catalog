using System.ComponentModel;

namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект ответа по статусу экземпляра книги
    /// </summary>
    public class BookInstanceStateResponse
    {

        /// <summary>
        /// ИД записи экземпляра книги
        /// </summary>        
        public int Id { get; set; }

        /// <summary>
        /// ИД статуса состояния книги
        /// </summary>        
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
