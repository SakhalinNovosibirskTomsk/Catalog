namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект ответа по состоянию списания экземпляра книги
    /// </summary>
    public class BookInstanceIsWroteOffResponse
    {

        /// <summary>
        /// ИД записи экземпляра книги
        /// </summary>        
        public int Id { get; set; }

        /// <summary>
        /// Признак, что в данный момент экземпляр книги списан
        /// </summary>        
        public bool IsWroteOff { get; set; } = false;

    }
}
