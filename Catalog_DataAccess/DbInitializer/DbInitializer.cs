using Catalog_Domain.CatalogDB;

namespace Catalog_DataAccess.DbInitializer
{

    /// <summary>
    /// Инициализация БД - создание и наполнение начальными данными
    /// </summary>
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор инициализации БД значениями по умолчанию
        /// </summary>
        /// <param name="db">Контекст БД приложения</param>
        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Метод наполения БД значениями по умолчанию
        /// </summary>
        public void InitializeDb()
        {
            Console.WriteLine("Инициализация БД: Удаление БД ... ");
            _db.Database.EnsureDeleted();
            Console.WriteLine("Инициализация БД: Удаление БД - Выполнено");

            Console.WriteLine("Инициализация БД: Создание БД ... ");
            _db.Database.EnsureCreated();
            Console.WriteLine("Инициализация БД: Создание БД - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Authors ... ");
            FillTable<Author>(InitialDataFactory.Authors);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Authors - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Publishers ... ");
            //_db.AddRange(InitialDataFactory.Publishers);
            //_db.SaveChanges();
            FillTable<Publisher>(InitialDataFactory.Publishers);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Publishers - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы States ... ");
            FillTable<State>(InitialDataFactory.States);
            Console.WriteLine("Инициализация БД: Заполнение таблицы States - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Books ... ");
            FillTable<Book>(InitialDataFactory.Books);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Books - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы BookToAuthors ... ");
            FillTable<BookToAuthor>(InitialDataFactory.BookToAuthors);
            Console.WriteLine("Инициализация БД: Заполнение таблицы BookToAuthors - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы BookInstances ... ");
            FillTable<BookInstance>(InitialDataFactory.BookInstances);
            Console.WriteLine("Инициализация БД: Заполнение таблицы BookInstances - Выполнено");
        }


        /// <summary>
        /// Метод заполнения таблицы БД для определённой сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="tableList">Список данных для записи в БД</param>
        public void FillTable<T>(List<T> tableList)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in tableList)
                    {
                        _db.Add(item);
                    }
                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }
    }
}
