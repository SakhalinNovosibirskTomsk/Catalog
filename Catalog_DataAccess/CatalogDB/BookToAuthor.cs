using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSEF = Microsoft.EntityFrameworkCore;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Связь книги с автором
    /// </summary> 
    [Table("BookToAuthors")]
    [MSEF.Index("BookId", "AuthorId", IsUnique = true, Name = "BookIdAuthorId")]
    [MSEF.Comment("Связь книги с автором")]
    public class BookToAuthor : BaseEntity
    {
        /// <summary>
        /// ИД книги
        /// </summary>
        [Required]
        [MSEF.Comment("ИД книги")]
        public int BookId { get; set; }

        /// <summary>
        /// Книга
        /// </summary>
        [ForeignKey("BookId")]
        [MSEF.Comment("Книга")]
        public virtual Book Book { get; set; }

        /// <summary>
        /// ИД автора
        /// </summary>
        [Required]
        [MSEF.Comment("ИД автора")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [ForeignKey("AuthorId")]
        [MSEF.Comment("Автор")]
        public virtual Author Author { get; set; }

        /// <summary>
        /// ИД пользователя добавившего запись
        /// </summary>
        [Required]
        [MSEF.Comment("ИД пользователя добавившего запись")]
        public Guid AddUserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        [Required]
        [MSEF.Comment("Дата/время добавления записи")]
        public DateTime AddTime { get; set; } = DateTime.Now;

    }
}
