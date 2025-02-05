using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog_DataAccess.CatalogDB
{

    /// <summary>
    /// Связь книги с автором
    /// </summary> 
    [Table("BooksToAuthors")]
    [Index("BookId", "AuthorId", IsUnique = true, Name = "BookIdAuthorId")]
    [Comment("Связь книги с автором")]
    public class BookToAuthor : BaseEntity
    {
        /// <summary>
        /// ИД книги
        /// </summary>
        [Required]
        [Comment("ИД книги")]
        public int BookId { get; set; }

        /// <summary>
        /// Книга
        /// </summary>
        [ForeignKey("BookId")]
        [Comment("Книга")]
        public Book Book { get; set; }

        /// <summary>
        /// ИД автора
        /// </summary>
        [Required]
        [Comment("ИД автора")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [ForeignKey("AuthorId")]
        [Comment("Автор")]
        public Author Author { get; set; }

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

    }
}
