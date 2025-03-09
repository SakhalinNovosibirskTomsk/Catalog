namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Флаг, что экземпляр книги выдаётся только в читальный зал
    /// </summary>
    public class BookInstanceOnlyForReadingRoomResponse
    {
        /// <summary>
        /// Признак, что экземпляр книги можно выдавать только в читальный зал
        /// </summary>
        public bool OnlyForReadingRoom { get; set; } = false;
    }
}
