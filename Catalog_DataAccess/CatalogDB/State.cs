using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSEF = Microsoft.EntityFrameworkCore;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Справочник статусов состояния экземпляров книг
    /// </summary>
    [Table("States")]
    [MSEF.Comment("Справочник статусов состояния экземпляров книг")]
    [MSEF.Index("Name", IsUnique = true, Name = "Name")]
    public class State : BaseEntity
    {

        /// <summary>
        /// Наименование статуса состояния экземпляра книги
        /// </summary>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [MSEF.Comment("Наименование статуса состояния экземпляра книги")]
        public string Name { get; set; }

        /// <summary>
        /// Описание статуса состояния экземпляра книги
        /// </summary>
        [MaxLength(1000)]
        [MSEF.Comment("Описание статуса состояния экземпляра книги")]
        public string? Description { get; set; }

        /// <summary>
        /// Признак, что состояние является исходным (например, присваивается по умолчанию при поступлении нового экземпляра книги)
        /// </summary>
        [MSEF.Comment("Признак, что состояние является исходным (например, присваивается по умолчанию при поступлении нового экземпляра книги)")]
        public bool IsInitialState { get; set; } = false;

        /// <summary>
        /// Признак, что при выставлении данного состояния экземпляру книги (например, при возврате) требуется обязательный комментарий
        /// </summary>
        [MSEF.Comment("Признак, что при выставлении данного состояния экземпляру книги (например, при возврате) требуется обязательный комментарий")]
        public bool IsNeedComment { get; set; } = false;

        /// <summary>
        /// Признак удаления записи в архив
        /// </summary>
        [MSEF.Comment("Признак удаления записи в архив")]
        public bool IsArchive { get; set; }

    }
}
