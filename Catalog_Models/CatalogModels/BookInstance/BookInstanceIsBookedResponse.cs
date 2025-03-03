namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект ответа по состоянию бронирования экземпляра книги
    /// </summary>
    public class BookInstanceIsBookedResponse
    {

        /// <summary>
        /// ИД записи экземпляра книги
        /// </summary>        
        public int Id { get; set; }

        /// <summary>
        /// Признак, что в данный момент экземпляр книги забронирован
        /// </summary>        
        public bool IsBooked { get; set; } = false;

    }
}
