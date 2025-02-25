namespace Catalog_Models.CatalogModels.BookToAuthor
{
    /// <summary>
    /// Автор - сокращённый ответ
    /// </summary>
    public class AuthorShortResponse
    {

        /// <summary>
        /// ИД автора
        /// </summary>        
        public int Id { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>        
        public string FullName { get; set; }

        /// <summary>
        /// Является ли запись архивной
        /// </summary>        
        public bool IsArchive { get; set; }

    }
}
