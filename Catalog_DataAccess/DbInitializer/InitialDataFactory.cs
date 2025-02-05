using Catalog_DataAccess.CatalogDB;

namespace Catalog_DataAccess.DbInitializer
{

    /// <summary>
    /// Данные для начального заполенния БД
    /// </summary>
    public static class InitialDataFactory
    {

        /// <summary>
        /// Авторы
        /// </summary>
        public static List<Author> Authors => new List<Author>()
        {
            //new Author()
            //{
            //    Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
            //    Email = "owner@somemail.ru",
            //    FirstName = "Иван",
            //    LastName = "Сергеев",
            //    Role = Roles.FirstOrDefault(x => x.Name == "Admin"),
            //    AppliedPromocodesCount = 5
            //},
            //new Author()
            //{
            //    Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
            //    Email = "andreev@somemail.ru",
            //    FirstName = "Петр",
            //    LastName = "Андреев",
            //    Role = Roles.FirstOrDefault(x => x.Name == "PartnerManager"),
            //    AppliedPromocodesCount = 10
            //},
        };

        /// <summary>
        /// Издатели
        /// </summary>
        public static List<Publisher> Publishers => new List<Publisher>()
        {
        };

        /// <summary>
        /// Статусы состояния экземпляров книги
        /// </summary>
        public static List<State> States => new List<State>()
        {
        };

        /// <summary>
        /// Книги
        /// </summary>
        public static List<Book> Books => new List<Book>()
        {
        };

        /// <summary>
        /// Экземпляры книг
        /// </summary>
        public static List<BookInstance> BookInstances => new List<BookInstance>()
        {
        };

        /// <summary>
        /// Прявязка авторов книг к книгам
        /// </summary>
        public static List<BookToAuthor> BooksToAuthors => new List<BookToAuthor>()
        {
        };
    }
}
