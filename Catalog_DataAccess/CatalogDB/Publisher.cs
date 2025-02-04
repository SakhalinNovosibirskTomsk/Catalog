using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Catalog_DataAccess.CatalogDB
{
    /// <summary>
    /// Справочник издателей
    /// </summary>
    [Table("Publishers")]
    [Index("Name", IsUnique = true, Name = "Name")]
    [Comment("Справочник издателей")]
    public class Publisher : BaseEntity
    {

        /// <summary>
        /// Наименование издателя
        /// </summary>
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        [Comment("Наименование издателя")]
        public string Name { get; set; }

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        [Required]
        [Comment("ИД пользователя добавившего запись об издателе")]
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
