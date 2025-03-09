namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Максимальное количество дней, на которое выдаётся экземпляр книги
    /// </summary>
    public class BookInstanceOutMaxDaysResponse
    {
        /// <summary>
        /// Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги
        /// </summary>
        public int OutMaxDays { get; set; } = 14;
    }
}
