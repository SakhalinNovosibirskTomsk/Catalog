namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект запроса обновления данныз о списании экземпляра книги
    /// </summary>
    public class BookInstanceUpdateWriteOffRequest
    {

        /// <summary>
        /// Дата списания экземпляра книги из библиотеки
        /// </summary>        
        public DateTime? WriteOffDate { get; set; } = null;

        /// <summary>
        /// ИД причины списания экземпляра книги из библиотеки
        /// </summary>        
        public int? WriteOffReasonId { get; set; } = null;


        /// <summary>
        /// ИД пользователя списавшего экземпляр книги
        /// </summary>        
        public Guid? WriteOffUserId { get; set; } = null;

    }
}
