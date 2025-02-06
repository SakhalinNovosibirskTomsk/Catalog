using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSEF = Microsoft.EntityFrameworkCore;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Справочник книг
    /// </summary>
    [Table("Books")]
    [MSEF.Index("Name", IsUnique = false, Name = "Name")]
    [MSEF.Comment("Книги")]
    public class Book : BaseEntity
    {
        /// <summary>
        /// Наименование книги
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        [MSEF.Comment("Наименование книги")]
        public string Name { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        [MaxLength(17)]
        [MSEF.Comment("The International Standard Book Number - Международный стандартный книжный номер")]
        public string? ISBN { get; set; }

        /// <summary>
        /// ИД издателя
        /// </summary>
        [Required]
        [MSEF.Comment("ИД издателя")]
        public int PublisherId { get; set; }

        /// <summary>
        /// Издатель
        /// </summary>
        [ForeignKey("PublisherId")]
        public virtual Publisher? Publisher { get; set; }

        /// <summary>
        /// Дата издания
        /// </summary>
        [MSEF.Comment("Дата издания")]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Ссылка на электронную версию книги
        /// </summary>
        [MaxLength(1000)]
        [MSEF.Comment("Ссылка на электронную версию книги")]
        public string? EBookLink { get; set; } = string.Empty;

        /// <summary>
        /// Количество скачиваний электронной версии книги
        /// </summary>
        [MSEF.Comment("Количество скачиваний электронной версии книги")]
        public int EBookDownloadCount { get; set; } = 0;

        /// <summary>
        /// ИД пользователя, добавившего книгу
        /// </summary>
        [Required]
        [MSEF.Comment("ИД пользователя добавившего книгу")]
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления книги
        /// </summary>
        [Required]
        [MSEF.Comment("Дата/время добавления книги")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Признак удаления книги в архив
        /// </summary>
        [MSEF.Comment("Признак удаления книги в архив")]
        public bool IsArchive { get; set; } = false;
    }
}
