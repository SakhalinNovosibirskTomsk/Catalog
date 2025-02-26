using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект создания/обновления экземпляра книги
    /// </summary>
    public class BookInstanceCreateUpdateRequest
    {
        /// <summary>
        /// ИД книги
        /// </summary>
        [Required]
        [DisplayName("ИД книги")]
        public int BookId { get; set; }

        /// <summary>
        /// Инвентарный номер экземпляра книги
        /// </summary>
        [Required]
        [DisplayName("Инвентарный номер экземпляра книги")]
        public string InventoryNumber { get; set; }

        /// <summary>
        /// Дата получения экземпляра книги в библиотеку
        /// </summary>
        [Required]
        [DisplayName("Дата получения экземпляра книги в библиотеку")]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;


        /// <summary>
        /// Признак, что экземпляр книги можно выдавать только в читальный зал
        /// </summary>
        [DisplayName("Признак, что экземпляр книги можно выдавать только в читальный зал")]
        public bool OnlyForReadingRoom { get; set; } = false;


        /// <summary>
        /// Признак, что в данный момент экземпляр книги выдан читателю
        /// </summary>        
        [DisplayName("Признак, что в данный момент экземпляр книги выдан читателю")]
        public bool IsCheckedOut { get; set; } = false;

        /// <summary>
        /// Дата списания экземпляра книги из библиотеки
        /// </summary>
        [DisplayName("Дата списания экземпляра книги из библиотеки")]
        public DateTime? WriteOffDate { get; set; } = null;

        /// <summary>
        /// ИД причины списания экземпляра книги из библиотеки
        /// </summary>
        [DisplayName("ИД причины списания экземпляра книги из библиотеки")]
        public int? WriteOffReasonId { get; set; } = null;


        /// <summary>
        /// ИД пользователя списавшего экземпляр книги
        /// </summary>        
        [DisplayName("ИД пользователя списавшего экземпляр книги")]
        public Guid? WriteOffUserId { get; set; } = null;

        /// <summary>
        /// ИД статуса состояния книги
        /// </summary>
        [Required]
        [DisplayName("ИД статуса состояния книги")]
        public int StateId { get; set; }

        /// <summary>
        /// ИД комментария к статусу состояния книги
        /// </summary>
        [DisplayName("ИД комментария к статусу состояния книги")]
        public int? FactCommentId { get; set; } = null;

        /// <summary>
        /// Текст комментария к статусу состояния книги
        /// </summary>
        [DisplayName("Текст комментария к статусу состояния книги")]
        public string? FactCommentText { get; set; } = String.Empty;

        /// <summary>
        /// Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги
        /// </summary>
        [Required]
        [DisplayName("Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги")]
        public int OutMaxDays { get; set; } = 14;

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        [Required]
        [DisplayName("ИД пользователя добавившего запись")]
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        [Required]
        [DisplayName("Дата/время добавления записи")]
        public DateTime AddTime { get; set; } = DateTime.Now;
    }
}
