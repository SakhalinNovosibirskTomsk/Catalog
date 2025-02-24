namespace Catalog_Models.CatalogModels.Author
{
    public class AuthorItemResponse
    {

        /// <summary>
        /// ИД автора
        /// </summary>        
        public int Id { get; set; }


        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string? MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Полное имя
        /// </summary>        
        public string FullName { get; set; }

        /// <summary>
        /// Является ли зарубежным автором
        /// </summary>        
        public bool IsForeign { get; set; }

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>        
        public Guid AddUserId { get; set; }

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// Является ли запись архивной
        /// </summary>        
        public bool IsArchive { get; set; }
    }
}
