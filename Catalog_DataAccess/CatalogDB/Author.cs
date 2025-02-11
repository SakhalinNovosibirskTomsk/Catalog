using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSEF = Microsoft.EntityFrameworkCore;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Справочник авторов
    /// </summary>
    [Table("Authors")]
    [MSEF.Index("FirstName", "LastName", "MiddleName", IsUnique = true, Name = "FullName")]
    [MSEF.Comment("Справочник авторов книг")]
    public class Author : BaseEntity
    {

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [MSEF.Comment("Имя автора")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [MSEF.Comment("Фамилия автора")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(200)]
        [MSEF.Comment("Отчество автора")]
        public string? MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Полное имя
        /// </summary>
        [NotMapped]
        public string FullName => FirstName + (String.IsNullOrEmpty(MiddleName) ? " " : " " + MiddleName.Trim() + " ") + LastName;

        /// <summary>
        /// Является ли зарубежным автором
        /// </summary>
        [MSEF.Comment("Признак является ли зарубежным автором")]
        public bool IsForeign { get; set; } = false;

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        [Required]
        [MSEF.Comment("ИД пользователя добавившего запись")]
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        [Required]
        [MSEF.Comment("Дата/время добавления записи")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Является ли запись аврхивной
        /// </summary>
        [MSEF.Comment("Признак является ли запись архивной")]
        public bool IsArchive { get; set; } = false;
    }
}


