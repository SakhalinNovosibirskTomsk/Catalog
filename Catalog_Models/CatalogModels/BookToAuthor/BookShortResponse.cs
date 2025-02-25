namespace Catalog_Models.CatalogModels.BookToAuthor
{
    /// <summary>
    /// Книга - сокращённый ответ
    /// </summary>
    public class BookShortResponse
    {

        /// <summary>
        /// ИД книги
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование книги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string? ISBN { get; set; }

        /// <summary>
        /// Признак удаления книги в архив
        /// </summary>        
        public bool IsArchive { get; set; } = false;


    }
}
