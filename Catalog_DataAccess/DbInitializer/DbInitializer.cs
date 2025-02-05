namespace Catalog_DataAccess.DbInitializer
{

    /// <summary>
    /// Инициализация БД - создание и наполнение начальными данными
    /// </summary>
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }

        public void InitializeDb()
        {
            Console.WriteLine("Инициализация БД: Удаление БД ... ");
            _db.Database.EnsureDeleted();
            Console.WriteLine("Инициализация БД: Удаление БД - Выполнено");

            Console.WriteLine("Инициализация БД: Создание БД ... ");
            _db.Database.EnsureCreated();
            Console.WriteLine("Инициализация БД: Создание БД - Выполнено");


            Console.WriteLine("Инициализация БД: Заполнение таблицы Authors ... ");
            _db.AddRange(InitialDataFactory.Authors);
            _db.SaveChanges();
            Console.WriteLine("Инициализация БД: Заполнение таблицы Authors - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Publishers ... ");
            _db.AddRange(InitialDataFactory.Publishers);
            _db.SaveChanges();
            Console.WriteLine("Инициализация БД: Заполнение таблицы Publishers - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы States ... ");
            _db.AddRange(InitialDataFactory.States);
            _db.SaveChanges();
            Console.WriteLine("Инициализация БД: Заполнение таблицы States - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Books ... ");
            _db.AddRange(InitialDataFactory.Books);
            _db.SaveChanges();
            Console.WriteLine("Инициализация БД: Заполнение таблицы Books - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы BooksToAuthors ... ");
            _db.AddRange(InitialDataFactory.BooksToAuthors);
            _db.SaveChanges();
            Console.WriteLine("Инициализация БД: Заполнение таблицы BooksToAuthors - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы BookInstances ... ");
            _db.AddRange(InitialDataFactory.BookInstances);
            _db.SaveChanges();
            Console.WriteLine("Инициализация БД: Заполнение таблицы BookInstances - Выполнено");
        }
    }
}
