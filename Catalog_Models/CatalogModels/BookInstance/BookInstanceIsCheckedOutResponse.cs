namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект ответа на запрос о том взят ли экземпляр книги из библиотеки или нет
    /// </summary>
    public class BookInstanceIsCheckedOutResponse
    {

        /// <summary>
        /// ИД записи экземпляра книги
        /// </summary>        
        public int Id { get; set; }

        /// <summary>
        /// Признак, что в данный момент экземпляр книги выдан читателю
        /// </summary>        
        public bool IsCheckedOut { get; set; } = false;
    }
}
