using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Справочник книг
    /// </summary>
    [Table("Books")]
    [Index("Name", IsUnique = false, Name = "Name")]
    [Comment("Книги")]
    public class Book : BaseEntity
    {
        /// <summary>
        /// Наименование книги
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        [Comment("Наименование книги")]
        public string Name { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        [MaxLength(13)]
        [Comment("The International Standard Book Number - Международный стандартный книжный номер")]
        public string ISBN { get; set; }

        /// <summary>
        /// ИД издателя
        /// </summary>
        [Required]
        [Comment("ИД издателя")]
        public int PublisherId { get; set; }

        /// <summary>
        /// Издатель
        /// </summary>
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Дата издания
        /// </summary>
        [Comment("Дата издания")]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Ссылка на электронную версию книги
        /// </summary>
        [MaxLength(1000)]
        [Comment("Ссылка на электронную версию книги")]
        public string EBookLink { get; set; } = string.Empty;

        /// <summary>
        /// Количество скачиваний электронной версии книги
        /// </summary>
        [Comment("Количество скачиваний электронной версии книги")]
        public int EBookDownloadCount { get; set; } = 0;

        /// <summary>
        /// ИД пользователя, добавившего книгу
        /// </summary>
        [Required]
        [Comment("ИД пользователя добавившего книгу")]
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления книги
        /// </summary>
        [Required]
        [Comment("Дата/время добавления книги")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Признак удаления книги в архив
        /// </summary>
        [Comment("Признак удаления книги в архив")]
        public bool IsArchive { get; set; } = false;


    }
}
