﻿using Catalog_Models.CatalogModels.BookToAuthor;
using System.ComponentModel.DataAnnotations;

namespace Catalog_Models.CatalogModels.BookInstance
{
    /// <summary>
    /// Объект создания/обновления экземпляра книги
    /// </summary>
    public class BookInstanceResponse
    {

        /// <summary>
        /// ИД записи
        /// </summary>        
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Книга
        /// </summary>
        public BookShortResponse Book { get; set; }

        /// <summary>
        /// Инвентарный номер экземпляра книги
        /// </summary>
        public string InventoryNumber { get; set; }

        /// <summary>
        /// Признак, что экземпляр книги можно выдавать только в читальный зал
        /// </summary>
        public bool OnlyForReadingRoom { get; set; } = false;

        /// <summary>
        /// Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги
        /// </summary>
        public int OutMaxDays { get; set; } = 14;

    }
}
