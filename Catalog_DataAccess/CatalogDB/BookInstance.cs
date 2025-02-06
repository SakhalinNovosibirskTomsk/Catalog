using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Экземпляры книг
    /// </summary>
    [Table("BookInstances")]
    [Comment("Экземпляры книг")]
    public class BookInstance : BaseEntity
    {
        /// <summary>
        /// ИД книги
        /// </summary>
        [Required]
        [Comment("ИД книги")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }


        /// <summary>
        /// Инвентарный номер экземпляра книги
        /// </summary>
        [Required]
        [MaxLength(20)]
        [MinLength(1)]
        [Comment("Инвентарный номер экземпляра книги")]
        public string InventoryNumber { get; set; }

        /// <summary>
        /// Дата получения экземпляра книги в библиотеку
        /// </summary>
        [Required]
        [Comment("Дата получения экземпляра книги в библиотеку")]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;


        /// <summary>
        /// Признак, что экземпляр книги можно выдавать только в читальный зал
        /// </summary>
        [Comment("Признак, что экземпляр книги можно выдавать только в читальный зал")]
        public bool OnlyForReadingRoom { get; set; } = false;


        /// <summary>
        /// Признак, что в данный момент экземпляр книги выдан читателю
        /// </summary>
        [Comment("Признак, что в данный момент экземпляр книги выдан читателю")]
        public bool IsCheckedOut { get; set; } = false;

        /// <summary>
        /// Дата списания экземпляра книги из библиотеку
        /// </summary>
        [Comment("Дата списания экземпляра книги из библиотеку")]
        public DateTime? WriteOffDate { get; set; }

        /// <summary>
        /// ИД причины списания экземпляра книги из библиотеки
        /// </summary>
        [Comment("ИД причины списания экземпляра книги из библиотеки")]
        public int? WriteOffReasonId { get; set; }


        /// <summary>
        /// ИД пользователя списавшего экземпляр книги
        /// </summary>
        [Comment("ИД пользователя списавшего экземпляр книги")]
        public Guid? WriteOffUserId { get; set; }

        /// <summary>
        /// ИД статуса состояния книги
        /// </summary>
        [Required]
        [Comment("ИД статуса состояния книги")]
        public int StateId { get; set; }

        /// <summary>
        /// Статус книги
        /// </summary>
        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        /// <summary>
        /// ИД комментария к статусу состояния книги
        /// </summary>
        [Comment("ИД комментария к статусу состояния книги")]
        public int? FactCommentId { get; set; }

        /// <summary>
        /// Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги
        /// </summary>
        [Required]
        [Comment("Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги")]
        public int OutMaxDays { get; set; } = 14;

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
        /// Признак удаления записи в архив
        /// </summary>
        [Comment("Признак удаления записи в архив")]
        public bool IsArchive { get; set; } = false;

    }
}



