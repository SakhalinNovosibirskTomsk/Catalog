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
        /// Признак, что экземпляр книги можно выдавать только в читальный зал
        /// </summary>
        [DisplayName("Признак, что экземпляр книги можно выдавать только в читальный зал")]
        public bool OnlyForReadingRoom { get; set; } = false;

        /// <summary>
        /// Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги
        /// </summary>
        [Required]
        [DisplayName("Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги")]
        public int OutMaxDays { get; set; } = 14;
    }
}
