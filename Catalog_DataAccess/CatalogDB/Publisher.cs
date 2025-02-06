using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSEF = Microsoft.EntityFrameworkCore;
namespace Catalog_DataAccess.CatalogDB
{
    /// <summary>
    /// Справочник издателей
    /// </summary>
    [Table("Publishers")]
    [MSEF.Index("Name", IsUnique = true, Name = "Name")]
    [MSEF.Comment("Справочник издателей")]
    public class Publisher : BaseEntity
    {

        /// <summary>
        /// Наименование издателя
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        [MSEF.Comment("Наименование издателя")]
        public string Name { get; set; }

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        [Required]
        [MSEF.Comment("ИД пользователя добавившего запись об издателе")]
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        [Required]
        [MSEF.Comment("Дата/время добавления записи")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Признак удаления записи в архив
        /// </summary>
        [MSEF.Comment("Признак удаления записи в архив")]
        public bool IsArchive { get; set; } = false;
    }
}
