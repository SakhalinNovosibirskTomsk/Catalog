using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog_Domain.CatalogDB
{

    /// <summary>
    /// Справочник авторов
    /// </summary>
    public class Author : BaseEntity
    {

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
        [NotMapped]
        public string FullName => FirstName + (String.IsNullOrWhiteSpace(MiddleName) ? " " : " " + MiddleName.Trim() + " ") + LastName;

        /// <summary>
        /// Является ли зарубежным автором
        /// </summary>        
        public bool IsForeign { get; set; } = false;

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Является ли запись аврхивной
        /// </summary>        
        public bool IsArchive { get; set; } = false;

        public virtual ICollection<BookToAuthor> BookToAuthorList { get; set; }
    }
}


