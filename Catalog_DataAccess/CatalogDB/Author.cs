using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Справочник авторов
    /// </summary>
    [Table("Authors")]
    [Index("FirstName", "LastName", "MiddleName", IsUnique = true, Name = "FullName")]
    [Comment("Справочник авторов книг")]
    public class Author : BaseEntity
    {

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [Comment("Имя автора")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [Comment("Фамилия автора")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(200)]
        [Comment("Отчество автора")]
        public string? MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Полное имя
        /// </summary>
        [NotMapped]
        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        /// <summary>
        /// Является ли зарубежным автором
        /// </summary>
        [Comment("Признак является ли зарубежным автором")]
        public bool IsForeign { get; set; } = false;

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        [Required]
        [Comment("ИД пользователя добавившего запись")]
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        [Required]
        [Comment("Дата/время добавления записи")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Является ли запись аврхивной
        /// </summary>
        [Comment("Признак является ли запись аврхивной")]
        public bool IsArchive { get; set; } = false;
    }
}


