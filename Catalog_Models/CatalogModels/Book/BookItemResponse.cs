using Catalog_Models.CatalogModels.Author;
using Catalog_Models.CatalogModels.Publisher;

namespace Catalog_Models.CatalogModels.Book
{
    public class BookItemResponse
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
        /// Издатель
        /// </summary>
        public PublisherItemResponse? Publisher { get; set; }

        /// <summary>
        /// Дата издания
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Ссылка на электронную версию книги
        /// </summary>
        public string? EBookLink { get; set; } = string.Empty;

        /// <summary>
        /// Количество скачиваний электронной версии книги
        /// </summary>        
        public int EBookDownloadCount { get; set; } = 0;

        /// <summary>
        /// ИД пользователя, добавившего книгу
        /// </summary>        
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления книги
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Авторы книги
        /// </summary>
        public List<AuthorItemResponse> BookToAuthorList { get; set; }

        /// <summary>
        /// Признак удаления книги в архив
        /// </summary>        
        public bool IsArchive { get; set; } = false;
    }
}
