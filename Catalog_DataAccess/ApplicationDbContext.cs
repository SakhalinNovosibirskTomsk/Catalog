using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_DataAccess
{

    /// <summary>
    /// DbContext
    /// </summary>
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if (SD.dbConnectionMode == SD.DbConnectionMode.PostgreSQL)
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        /// <summary>
        /// Авторы
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Книги
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Экземпляры книг
        /// </summary>
        public DbSet<BookInstance> BookInstances { get; set; }

        /// <summary>
        /// Издатели
        /// </summary>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Статусы состояния экземпляра книги
        /// </summary>
        public DbSet<State> States { get; set; }

        /// <summary>
        /// Связь книг с акторами
        /// </summary>
        public DbSet<BookToAuthor> BookToAuthors { get; set; }

    }

}
