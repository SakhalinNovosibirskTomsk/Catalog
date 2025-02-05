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
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();

            _db.AddRange(InitialDataFactory.Authors);
            _db.SaveChanges();

            _db.AddRange(InitialDataFactory.Publishers);
            _db.SaveChanges();

            _db.AddRange(InitialDataFactory.States);
            _db.SaveChanges();

            _db.AddRange(InitialDataFactory.Books);
            _db.SaveChanges();

            _db.AddRange(InitialDataFactory.BookInstances);
            _db.SaveChanges();

            _db.AddRange(InitialDataFactory.BooksToAuthors);
            _db.SaveChanges();
        }
    }
}
