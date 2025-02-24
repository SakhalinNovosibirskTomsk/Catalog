using Catalog_DataAccess.CatalogDB;

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

            //using (var transaction = _db.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        if (SD.dbConnectionMode == SD.DbConnectionMode.MSSQL)
            //        {
            //            _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT " + typeof(Author).Name + "s ON;");
            //            _db.SaveChanges();
            //        }
            //        _db.AddRange(InitialDataFactory.Authors);
            //        _db.SaveChanges();
            //        transaction.Commit();
            //    }
            //    catch (Exception)
            //    {
            //        transaction.Rollback();
            //        throw;
            //    }
            //    finally
            //    {
            //        if (SD.dbConnectionMode == SD.DbConnectionMode.MSSQL)
            //        {
            //            _db.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo." + typeof(Author).Name + "s OFF;");
            //            _db.SaveChanges();
            //        }
            //    }
            //}

            Console.WriteLine("Инициализация БД: Заполнение таблицы Authors ... ");
            FillTable<Author>(InitialDataFactory.Authors);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Authors - Выполнено");

            //_db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors ON");
            //_db.SaveChanges();
            //_db.AddRange(InitialDataFactory.Authors);
            //_db.SaveChanges();
            //_db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors ON;");


            Console.WriteLine("Инициализация БД: Заполнение таблицы Publishers ... ");
            //_db.AddRange(InitialDataFactory.Publishers);
            //_db.SaveChanges();
            FillTable<Publisher>(InitialDataFactory.Publishers);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Publishers - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы States ... ");
            FillTable<State>(InitialDataFactory.States);
            //_db.AddRange(InitialDataFactory.States);
            //_db.SaveChanges();
            Console.WriteLine("Инициализация БД: Заполнение таблицы States - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Books ... ");
            //_db.AddRange(InitialDataFactory.Books);
            //_db.SaveChanges();
            FillTable<Book>(InitialDataFactory.Books);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Books - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы BookToAuthors ... ");
            //_db.AddRange(InitialDataFactory.BooksToAuthors);
            //_db.SaveChanges();
            FillTable<BookToAuthor>(InitialDataFactory.BookToAuthors);
            Console.WriteLine("Инициализация БД: Заполнение таблицы BookToAuthors - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы BookInstances ... ");
            //_db.AddRange(InitialDataFactory.BookInstances);
            //_db.SaveChanges();
            FillTable<BookInstance>(InitialDataFactory.BookInstances);
            Console.WriteLine("Инициализация БД: Заполнение таблицы BookInstances - Выполнено");
        }


        public void FillTable<T>(List<T> tableList)
        {
            Console.WriteLine("Инициализация БД: Заполнение таблицы Authors ... ");
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
