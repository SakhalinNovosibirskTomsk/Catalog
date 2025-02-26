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
        public DateTime WriteOffDate { get; set; }

        /// <summary>
        /// ИД причины списания экземпляра книги из библиотеки
        /// </summary>        
        public int WriteOffReasonId { get; set; }


        /// <summary>
        /// ИД пользователя списавшего экземпляр книги
        /// </summary>        
        public Guid WriteOffUserId { get; set; }

    }
}
