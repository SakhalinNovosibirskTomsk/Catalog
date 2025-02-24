using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Базовый класс сущностей
    /// </summary>    
    public class BaseEntity
    {
        /// <summary>
        /// ИД сущности
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("ИД записи")]
        public int Id { get; set; }
    }
}
