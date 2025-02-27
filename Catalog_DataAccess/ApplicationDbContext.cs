using Catalog_Domain.CatalogDB;
using Microsoft.EntityFrameworkCore;

namespace Catalog_DataAccess
{

    /// <summary>
    /// DbContext
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// DbContext приложения - конструктор
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
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


        /// <summary>
        /// Настройка сопоставления модели данных с БД
        /// </summary>
        /// <param name="modelBuilder">Объект построителя модели</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO разнести для каждой сужности в отдельный класс или метод настройку БД

            //------------------------------------------------------------------
            // Статусы
            //------------------------------------------------------------------
            modelBuilder.Entity<State>()
                .ToTable("States")
                .ToTable(t => t.HasComment("Справочник статусов состояния экземпляров книг"));


            modelBuilder.Entity<State>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<State>()
                .HasIndex(nameof(State.Name))
                .IsUnique()
                .HasDatabaseName(nameof(State) + nameof(State.Name));

            modelBuilder.Entity<State>()
                .Property(u => u.Id)
                .HasComment("ИД записи")
                .IsRequired();

            modelBuilder.Entity<State>()
                .Property(u => u.Name)
                .HasComment("Наименование статуса состояния экземпляра книги")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<State>()
                .Property(u => u.Description)
                .HasComment("Описание статуса состояния экземпляра книги")
                .HasMaxLength(1000);

            modelBuilder.Entity<State>()
                .Property(u => u.IsInitialState)
                .HasComment("Признак, что состояние является исходным (например, присваивается по умолчанию при поступлении нового экземпляра книги)")
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<State>()
                .Property(u => u.IsNeedComment)
                .HasComment("Признак, что при выставлении данного состояния экземпляру книги (например, при возврате) требуется обязательный комментарий")
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<State>()
                .Property(u => u.IsArchive)
                .HasComment("Признак удаления записи в архив")
                .IsRequired()
                .HasDefaultValue(false);
            //------------------------------------------------------------------

            //------------------------------------------------------------------
            // Издатели
            //------------------------------------------------------------------
            modelBuilder.Entity<Publisher>()
                .ToTable("Publishers")
                .ToTable(t => t.HasComment("Справочник издателей"));

            modelBuilder.Entity<Publisher>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Publisher>()
                .HasIndex(nameof(Publisher.Name))
                .IsUnique()
                .HasDatabaseName(nameof(Publisher) + nameof(Publisher.Name));

            modelBuilder.Entity<Publisher>()
                .Property(u => u.Id)
                .HasComment("ИД записи")
                .IsRequired();

            modelBuilder.Entity<Publisher>()
                .Property(u => u.Name)
                .HasComment("Наименование издателя")
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<Publisher>()
                .Property(u => u.AddUserId)
                .HasComment("ИД пользователя добавившего запись об издателе")
                .IsRequired();

            modelBuilder.Entity<Publisher>()
                .Property(u => u.AddTime)
                .HasComment("Дата/время добавления записи")
                .IsRequired();

            modelBuilder.Entity<Publisher>()
                .Property(u => u.IsArchive)
                .HasComment("Признак удаления записи в архив")
                .IsRequired()
                .HasDefaultValue(false);
            //------------------------------------------------------------------


            //------------------------------------------------------------------
            // Авторы
            //------------------------------------------------------------------
            modelBuilder.Entity<Author>()
                .ToTable("Authors")
                .ToTable(t => t.HasComment("Справочник авторов книг"));

            modelBuilder.Entity<Author>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Author>()
                .HasIndex(nameof(Author.FirstName), nameof(Author.LastName), nameof(Author.MiddleName))
                .IsUnique()
                .HasDatabaseName(nameof(Author) + "FullName");

            modelBuilder.Entity<Author>()
                .Property(u => u.Id)
                .HasComment("ИД записи")
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(u => u.FirstName)
                .HasComment("Имя автора")
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Author>()
                .Property(u => u.LastName)
                .HasComment("Фамилия автора")
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Author>()
                .Property(u => u.MiddleName)
                .HasComment("Отчество автора")
                .HasMaxLength(200);


            modelBuilder.Entity<Author>()
                .Property(u => u.IsForeign)
                .HasComment("Признак является ли зарубежным автором")
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(u => u.AddUserId)
                .HasComment("ИД пользователя добавившего запись")
                .IsRequired();

            modelBuilder.Entity<Publisher>()
                .Property(u => u.AddTime)
                .HasComment("Дата/время добавления записи")
                .IsRequired();

            modelBuilder.Entity<Publisher>()
                .Property(u => u.IsArchive)
                .HasComment("Признак удаления записи в архив")
                .IsRequired()
                .HasDefaultValue(false);
            //------------------------------------------------------------------

            //------------------------------------------------------------------
            // Связка книг с авторами
            //------------------------------------------------------------------
            modelBuilder.Entity<BookToAuthor>()
                .ToTable("BookToAuthors")
                .ToTable(t => t.HasComment("Связь книг с авторами"));

            modelBuilder.Entity<BookToAuthor>()
                .HasOne(b => b.Book)
                .WithMany(b => b.BookToAuthorList)
                .HasForeignKey(k => k.BookId);

            modelBuilder.Entity<BookToAuthor>()
                .HasOne(b => b.Author)
                .WithMany(b => b.BookToAuthorList)
                .HasForeignKey(k => k.AuthorId);

            modelBuilder.Entity<BookToAuthor>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<BookToAuthor>()
                .HasIndex(nameof(BookToAuthor.BookId), nameof(BookToAuthor.AuthorId))
                .IsUnique()
                .HasDatabaseName("BookIdAuthorId");

            modelBuilder.Entity<BookToAuthor>()
                .Property(u => u.Id)
                .HasComment("ИД записи")
                .IsRequired();

            modelBuilder.Entity<BookToAuthor>()
                .Property(u => u.BookId)
                .HasComment("ИД книги")
                .IsRequired();

            modelBuilder.Entity<BookToAuthor>()
                .Property(u => u.AuthorId)
                .HasComment("ИД автора")
                .IsRequired();

            modelBuilder.Entity<BookToAuthor>()
                .Property(u => u.AddUserId)
                .HasComment("ИД пользователя добавившего запись")
                .IsRequired();

            modelBuilder.Entity<BookToAuthor>()
                .Property(u => u.AddTime)
                .HasComment("Дата/время добавления записи")
                .IsRequired();

            //------------------------------------------------------------------

            //------------------------------------------------------------------
            // Книги
            //------------------------------------------------------------------
            modelBuilder.Entity<Book>()
                .ToTable("Books")
                .ToTable(t => t.HasComment("Справочник книг"))
                .HasOne(p => p.Publisher)
                .WithMany(b => b.BookList)
                .HasForeignKey(k => k.PublisherId);


            modelBuilder.Entity<Book>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Book>()
                .HasIndex(nameof(Book.Name))
                .IsUnique()
                .HasDatabaseName(nameof(Book) + nameof(Book.Name));

            modelBuilder.Entity<Book>()
                .Property(u => u.Id)
                .HasComment("ИД записи")
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(u => u.Name)
                .HasComment("Наименование книги")
                .HasMaxLength(300)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(u => u.ISBN)
                .HasComment("The International Standard Book Number - Международный стандартный книжный номер")
                .HasMaxLength(17);

            modelBuilder.Entity<Book>()
                .Property(u => u.PublisherId)
                .HasComment("ИД издателя")
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(u => u.PublishDate)
                .HasComment("Дата издания")
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(u => u.EBookLink)
                .HasComment("Количество скачиваний электронной версии книги");

            modelBuilder.Entity<Book>()
                .Property(u => u.AddUserId)
                .HasComment("ИД пользователя добавившего книгу")
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(u => u.AddTime)
                .HasComment("Дата/время добавления книги")
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(u => u.IsArchive)
                .HasComment("Признак удаления книги в архив")
                .IsRequired()
                .HasDefaultValue(false);


            //------------------------------------------------------------------

            //------------------------------------------------------------------
            // Экземпляры книг
            //------------------------------------------------------------------
            modelBuilder.Entity<BookInstance>()
                .ToTable("BookInstances")
                .ToTable(t => t.HasComment("Экземпляры книг"))
                .HasOne(s => s.State)
                .WithMany(b => b.BookInstanceList)
                .HasForeignKey(k => k.StateId);

            modelBuilder.Entity<BookInstance>()
                .HasOne(s => s.Book)
                .WithMany(b => b.BookInstanceList)
                .HasForeignKey(k => k.BookId);

            modelBuilder.Entity<BookInstance>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.Id)
                .HasComment("ИД записи")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.BookId)
                .HasComment("ИД книги")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.InventoryNumber)
                .HasComment("Инвентарный номер экземпляра книги")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.ReceiptDate)
                .HasComment("Дата получения экземпляра книги в библиотеку")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.OnlyForReadingRoom)
                .HasComment("Признак, что экземпляр книги можно выдавать только в читальный зал")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.IsCheckedOut)
                .HasComment("Признак, что в данный момент экземпляр книги выдан читателю")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.WriteOffDate)
                .HasComment("Дата списания экземпляра книги из библиотеку");

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.WriteOffReasonId)
                .HasComment("ИД причины списания экземпляра книги из библиотеки");

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.WriteOffUserId)
                .HasComment("ИД пользователя списавшего экземпляр книги");

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.StateId)
                .HasComment("ИД статуса состояния книги")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.FactCommentId)
                .HasComment("ИД комментария к статусу состояния книги");

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.FactCommentText)
                .HasComment("Текст комментария к статусу состояния книги");

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.OutMaxDays)
                .HasComment("Максимальное кол-во дней, на которые можно выдать читателю экземпляр книги")
                .IsRequired()
                .HasDefaultValue(14);

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.AddUserId)
                .HasComment("ИД пользователя добавившего запись")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.AddTime)
                .HasComment("Дата/время добавления записи")
                .IsRequired();

            modelBuilder.Entity<BookInstance>()
                .Property(u => u.IsArchive)
                .HasComment("Признак удаления записи в архив")
                .IsRequired()
                .HasDefaultValue(false);

            //------------------------------------------------------------------
        }
    }
}
