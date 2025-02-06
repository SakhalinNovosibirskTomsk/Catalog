using Catalog_Common;
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
            new Author()
            {
                Id = 1,
                FirstName = "Фёдор",
                LastName = "Абрамов",
                MiddleName = "Александрович",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 2,
                FirstName = "Кобо",
                LastName = "Абэ",
                MiddleName = "",
                IsForeign = true,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 3,
                FirstName = "Аввакум",
                LastName = "Кондратьев",
                MiddleName = "Петрович",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 4,
                FirstName = "Аркадий",
                LastName = "Аверченко",
                MiddleName = "Тимофеевич",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 5,
                FirstName = "Григорий",
                LastName = "Адамов",
                MiddleName = "Борисович",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 6,
                FirstName = "Алесь",
                LastName = "Адамович",
                MiddleName = "Михайлович",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 7,
                FirstName = "Ричард",
                LastName = "Адамс",
                MiddleName = "",
                IsForeign = true,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 8,
                FirstName = "Айзек",
                LastName = "Азимов",
                MiddleName = "",
                IsForeign = true,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 9,
                FirstName = "Анатолий",
                LastName = "Азольский",
                MiddleName = "Алексеевич",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 10,
                FirstName = "Чингиз",
                LastName = "Айтматов",
                MiddleName = "Торекулович",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 11,
                FirstName = "Сергей",
                LastName = "Аксаков",
                MiddleName = "Тимофеевич",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 12,
                FirstName = "Василий",
                LastName = "Жуковский",
                MiddleName = "Андреевич",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 13,
                FirstName = "Василий",
                LastName = "Аксёнов",
                MiddleName = "Павлович",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 14,
                FirstName = "Рюноскэ",
                LastName = "Акутагава",
                MiddleName = "",
                IsForeign = true,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 15,
                FirstName = "Марк",
                LastName = "Алданов",
                MiddleName = "Александрович",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 16,
                FirstName = "Татьяна",
                LastName = "Александрова",
                MiddleName = "Ивановна",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 17,
                FirstName = "Светлана",
                LastName = "Алексиевич",
                MiddleName = "Александровна",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 18,
                FirstName = "Ален",
                LastName = "Фурнье",
                MiddleName = "",
                IsForeign = true,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 19,
                FirstName = "Маргарита",
                LastName = "Алигер",
                MiddleName = "Иосифовна",
                IsForeign = false,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 20,
                FirstName = "Жоржи",
                LastName = "Амаду",
                MiddleName = "",
                IsForeign = true,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

            new Author()
            {
                Id = 21,
                FirstName = "Мзечабук",
                LastName = "Амирэджиби",
                MiddleName = "",
                IsForeign = true,
                AddUserId = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive = false
            },

        };

        /// <summary>
        /// Издатели
        /// </summary>
        public static List<Publisher> Publishers => new List<Publisher>()
        {
            new Publisher
                {
                    Id = 1,
                    Name = "Лениздат",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
            new Publisher
                {
                    Id = 2,
                    Name = "Вагриус",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
            new Publisher
                {
                    Id = 3,
                    Name = "Эксмо",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
            new Publisher
                {
                    Id = 4,
                    Name = "Рипол Классик",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 5,
                    Name = "Азбука-Аттикус",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 6,
                    Name = "Терра - Книжный клуб",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 7,
                    Name = "Детская литература",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 8,
                    Name = "Оникс 21 век",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 9,
                    Name = "Азбука-классика",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 10,
                    Name = "Советский писатель",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 11,
                    Name = "Стрекоза",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 12,
                    Name = "АСТ",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 13,
                    Name = "Художественная литература",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 14,
                    Name = "Дрофа",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
             new Publisher
                {
                    Id = 15,
                    Name = "Время",
                    AddUserId = SD.UserIdForInitialData,
                    AddTime  = DateTime.Now,
                    IsArchive = false
                },
        };

        /// <summary>
        /// Статусы состояния экземпляров книги
        /// </summary>
        public static List<State> States => new List<State>()
        {
            new State
            {
                Id = 1,
                Name = "Отличное",
                Description = "Новая или \"как новая\". Присваивается при поступлении нового экземпляра книги в библиотеку. Остаётся до тех пор, пока книга "+
                                "не измент своё сотояние на отличное от изначального",
                IsInitialState = true,
                IsNeedComment  = false,
                IsArchive = false,
            },
             new State
            {
                Id = 2,
                Name = "Хорошее",
                Description = "Незначительные потёртости на обложке, пожелтение страниц и прочие \"повреждения временем\" "+
                                "без следов умышленной порчи или вследвие небрежного отношения",
                IsInitialState = false,
                IsNeedComment  = true,
                IsArchive = false,
             },
             new State
             {
                Id = 3,
                Name = "Среднее",
                Description = "Есть следы умышленного или вследсвие небрежного отношения повреждения - мятые страницы, небольшие (не больше 3 см) порывы страниц. "+
                               "Следы чернил, надписей или иных загрязнений на некоторых страницах и/или обложке, не носящие массовый характер и не " +
                               "мешающие восприятию текста, картинок, рисунков и прочего материала книги",
                IsInitialState = false,
                IsNeedComment  = true,
                IsArchive = false,
             },
             new State
             {
                Id = 4,
                Name = "Удовлетворительное",
                Description = "Экземпляр заметно отличается от изначального (нового). Загрязнения, рваные страницы, надписи/заметки на страницах и обложке. " +
                 "При этом состояние не откладывает существенного влияние на возможность восприятия информации, представленной в книге",
                IsInitialState = false,
                IsNeedComment  = true,
                IsArchive = false,
             },
             new State
             {
                Id = 5,
                Name = "Плохое",
                Description = "Рваные или вырваные страницы, сильные повреждения обложки, большое количество надписей и загрязнений. Инфоомация считываема, но " +
                    "некоторые предложения могут быть не полностью читаемы.",
                IsInitialState = false,
                IsNeedComment  = true,
                IsArchive = false,
             },
             new State
             {
                Id = 6,
                Name = "Под списание",
                Description = "Рваные или вырваные страницы. Некоторые страницы утеряны или информация на них невоспроизводима или воспроизводима " +
                    "со значительными затруднениями. Состояние, которое даже при редкости экземпляра книги не позволяет полноценно ей пользоваться " +
                    "и возникает вопрос о целесообразности хранения его в библиотеке",
                IsInitialState = false,
                IsNeedComment  = true,
                IsArchive = false,
             },
             new State
             {
                Id = 7,
                Name = "Устарела",
                Description = "Особый статус, обозначающий неактуальность информации отражённой к экземпляре вплоть до введения читателя в заблждение." +
                    "Например, научная литература, объясняющая теорию, которая из-за новых исследований опровергнута и её использование при подготовке " +
                    "к экзаменам или работам учащимися может привести к снижению оценки или не сдаче работы или предмета. " +
                    "Статус может быть похож на статус \"Под списание\", если специалисты по предмету решат, что наличие книги в библиотеке и возможность " +
                    "ознакомления с её содержимым учащимися несёт больше вреда, чем пользы от возможности удовлетворения академического интереса по изучению "+
                    "истоиии предмета или теории.",
                IsInitialState = false,
                IsNeedComment  = true,
                IsArchive = false,
             },
        };

        /// <summary>
        /// Книги
        /// </summary>
        public static List<Book> Books => new List<Book>()
        {
            new Book
            {
                Id = 1,
                Name = "Деревянные кони. Пелагея. Алька. Безотцовщина. Вокруг да около. Жила-была Семужка (сборник)",
                ISBN = "",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Лениздат").Id,
                PublishDate = new DateTime(1979),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },
            new Book
            {
                Id = 2,
                Name = "Две зимы и три лета",
                ISBN = "5-9560-0232-8",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Вагриус").Id,
                PublishDate = new DateTime(2004),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },
            new Book
            {
                Id = 3,
                Name = "Деревянные кони. Повести. Рассказы",
                ISBN = "978-5-699-52445-7",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Эксмо").Id,
                PublishDate = new DateTime(2011),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },
            new Book
            {
                Id = 4,
                Name = "Женщина в песках",
                ISBN = "5-7905-2393-5",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Рипол Классик").Id,
                PublishDate = new DateTime(2004),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },
            new Book
            {
                Id = 5,
                Name = "Житие протопопа Аввакума, им самим написанное, и другие его сочинения",
                ISBN = "978-5-389-02952-1",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Азбука-Аттикус").Id,
                PublishDate = new DateTime(2012),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },
            new Book
            {
                Id = 6,
                Name = "Собрание сочинений в 6 томах. Том 5. Чудеса в решете",
                ISBN = "978-5-275-01394-8",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Терра - Книжный клуб").Id,
                PublishDate = new DateTime(2007),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 7,
                Name = "Молодняк",
                ISBN = "5-08-002092-7",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Детская литература").Id,
                PublishDate = new DateTime(1992),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 8,
                Name = "Тайна двух океанов",
                ISBN = "5-329-00186-2",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Оникс 21 век").Id,
                PublishDate = new DateTime(2004),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 9,
                Name = "Хатынская повесть",
                ISBN = "978-5-9985-1061-8",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Азбука-классика").Id,
                PublishDate = new DateTime(2010),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 10,
                Name = "Хатынская повесть. Каратели (сборник)",
                ISBN = "",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Советский писатель").Id,
                PublishDate = new DateTime(1984),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 11,
                Name = "Обитатели холмов",
                ISBN = "978-5-699-53500-2",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Эксмо").Id,
                PublishDate = new DateTime(2011),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 12,
                Name = "Три закона роботехники (сборник)",
                ISBN = "5-04-002503-3",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Эксмо").Id,
                PublishDate = new DateTime(1999),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 13,
                Name = "Клетка",
                ISBN = "978-5-699-26098-0",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Эксмо").Id,
                PublishDate = new DateTime(2007),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 14,
                Name = "Белый пароход",
                ISBN = "978-5-699-26098-0",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Советский писатель").Id,
                PublishDate = new DateTime(1980),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 15,
                Name = "И дольше века длится день...",
                ISBN = "978-5-352-02231-3",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Азбука-классика").Id,
                PublishDate = new DateTime(2003),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 16,
                Name = "Пегий пес, бегущий краем моря (сборник)",
                ISBN = "5-352-00346-9",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Азбука-классика").Id,
                PublishDate = new DateTime(2005),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 17,
                Name = "Плаха",
                ISBN = "5-352-00714-6",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Азбука-классика").Id,
                PublishDate = new DateTime(2006),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 18,
                Name = "Тавро Кассандры",
                ISBN = "978-5-395-00046-0",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Азбука-классика").Id,
                PublishDate = new DateTime(2008),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 19,
                Name = "Аленький цветочек. Три пояса.",
                ISBN = "978-5-89537-741-3",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Стрекоза").Id,
                PublishDate = new DateTime(2008),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 20,
                Name = "Детские годы Багрова-внука",
                ISBN = "978-5-17-057197-0",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "АСТ").Id,
                PublishDate = new DateTime(2009),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 21,
                Name = "Семейная хроника",
                ISBN = "5-280-01789-2",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Художественная литература").Id,
                PublishDate = new DateTime(1991),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 22,
                Name = "Остров Крым",
                ISBN = "978-5-699-41765-0",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Эксмо").Id,
                PublishDate = new DateTime(2010),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 23,
                Name = "Новеллы (сборник)",
                ISBN = "5-280-00690-4",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Художественная литература").Id,
                PublishDate = new DateTime(1989),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 24,
                Name = "Заговор. Святая Елена, маленький остров. Повести (сборник)",
                ISBN = "5-15-000890-7",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "АСТ").Id,
                PublishDate = new DateTime(1998),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 25,
                Name = "Домовёнок Кузька (сборник)",
                ISBN = "5-17-011374-9",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "АСТ").Id,
                PublishDate = new DateTime(2003),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 26,
                Name = "У войны не женское лицо",
                ISBN = "978-5-9691-0229-3",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Время").Id,
                PublishDate = new DateTime(2007),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 27,
                Name = "Большой Мольн",
                ISBN = "",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Художественная литература").Id,
                PublishDate = new DateTime(1960),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 28,
                Name = "Маргарита Алигер. Избранное",
                ISBN = "",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Советский писатель").Id,
                PublishDate = new DateTime(1947),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 29,
                Name = "Генералы песчаных карьеров",
                ISBN = "978-S-17-066569-3",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "АСТ").Id,
                PublishDate = new DateTime(1947),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

            new Book
            {
                Id = 30,
                Name = "Дата Туташхиа",
                ISBN = "5-7107-0083-5",
                PublisherId = Publishers.FirstOrDefault(x => x.Name == "Дрофа").Id,
                PublishDate = new DateTime(1993),
                EBookLink = "",
                EBookDownloadCount = 0,
                AddUserId  = SD.UserIdForInitialData,
                AddTime = DateTime.Now,
                IsArchive  = false,
            },

        };

        /// <summary>
        /// Экземпляры книг
        /// </summary>
        public static List<BookInstance> BookInstances
        {
            get
            {
                var stateId = States.FirstOrDefault(x => x.Name == "Отличное").Id;
                var bookInstanceList = new List<BookInstance>();
                int bookInstanceIdcounter = 0;
                foreach (var book in Books)
                {
                    //var bookInstanceCounter = new Random().Next(20);

                    var bookInstanceCounter = 20;
                    for (var i = 1; i <= bookInstanceCounter; i++)
                    {
                        bookInstanceIdcounter++;
                        bookInstanceList.Add(
                            new BookInstance
                            {
                                Id = bookInstanceIdcounter,
                                BookId = book.Id,
                                InventoryNumber = "100001_" + bookInstanceIdcounter.ToString(),
                                ReceiptDate = DateTime.Now,
                                OnlyForReadingRoom = false,
                                IsCheckedOut = false,
                                WriteOffDate = null,
                                WriteOffReasonId = 1,
                                WriteOffUserId = null,
                                StateId = stateId,
                                FactCommentId = null,
                                OutMaxDays = 14,
                                AddUserId = SD.UserIdForInitialData,
                                AddTime = DateTime.Now,
                                IsArchive = false,
                            }
                        );
                    }
                }
                return bookInstanceList;
            }
        }

        /// <summary>
        /// Прявязка авторов книг к книгам
        /// </summary>
        public static List<BookToAuthor> BookToAuthors
        {
            get
            {
                var bookToAuthors = new List<BookToAuthor>();

                bookToAuthors.Add(
                    new BookToAuthor
                    {
                        Id = 1,
                        BookId = Books.FirstOrDefault(x => x.Name == "Деревянные кони. Пелагея. Алька. Безотцовщина. Вокруг да около. Жила-была Семужка (сборник)").Id,
                        AuthorId = Authors.FirstOrDefault(x => x.LastName == "Абрамов").Id,
                        AddUserId = SD.UserIdForInitialData,
                        AddTime = DateTime.Now,
                    });


                bookToAuthors.Add(
                        new BookToAuthor
                        {
                            Id = 2,
                            BookId = Books.FirstOrDefault(x => x.Name == "Две зимы и три лета").Id,
                            AuthorId = Authors.FirstOrDefault(x => x.LastName == "Абрамов").Id,
                            AddUserId = SD.UserIdForInitialData,
                            AddTime = DateTime.Now,
                        });

                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 3,
                             BookId = Books.FirstOrDefault(x => x.Name == "Деревянные кони. Повести. Рассказы").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Абрамов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 4,
                             BookId = Books.FirstOrDefault(x => x.Name == "Женщина в песках").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Абэ").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 5,
                             BookId = Books.FirstOrDefault(x => x.Name == "Житие протопопа Аввакума, им самим написанное, и другие его сочинения").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Кондратьев").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 6,
                             BookId = Books.FirstOrDefault(x => x.Name == "Собрание сочинений в 6 томах. Том 5. Чудеса в решете").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Аверченко").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 7,
                             BookId = Books.FirstOrDefault(x => x.Name == "Молодняк").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Аверченко").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 8,
                             BookId = Books.FirstOrDefault(x => x.Name == "Тайна двух океанов").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Адамов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 9,
                             BookId = Books.FirstOrDefault(x => x.Name == "Хатынская повесть").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Адамович").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 10,
                             BookId = Books.FirstOrDefault(x => x.Name == "Хатынская повесть. Каратели (сборник)").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Адамович").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 11,
                             BookId = Books.FirstOrDefault(x => x.Name == "Обитатели холмов").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Адамс").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 12,
                             BookId = Books.FirstOrDefault(x => x.Name == "Три закона роботехники (сборник)").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Азимов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 13,
                             BookId = Books.FirstOrDefault(x => x.Name == "Клетка").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Азольский").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });

                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 14,
                             BookId = Books.FirstOrDefault(x => x.Name == "Белый пароход").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Айтматов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 15,
                             BookId = Books.FirstOrDefault(x => x.Name == "И дольше века длится день...").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Айтматов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 16,
                             BookId = Books.FirstOrDefault(x => x.Name == "Пегий пес, бегущий краем моря (сборник)").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Айтматов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 17,
                             BookId = Books.FirstOrDefault(x => x.Name == "Плаха").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Айтматов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 18,
                             BookId = Books.FirstOrDefault(x => x.Name == "Тавро Кассандры").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Айтматов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 19,
                             BookId = Books.FirstOrDefault(x => x.Name == "Аленький цветочек. Три пояса.").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Аксаков").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 20,
                             BookId = Books.FirstOrDefault(x => x.Name == "Аленький цветочек. Три пояса.").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Жуковский").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 21,
                             BookId = Books.FirstOrDefault(x => x.Name == "Детские годы Багрова-внука").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Аксаков").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 22,
                             BookId = Books.FirstOrDefault(x => x.Name == "Семейная хроника").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Аксаков").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 23,
                             BookId = Books.FirstOrDefault(x => x.Name == "Остров Крым").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Аксёнов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 24,
                             BookId = Books.FirstOrDefault(x => x.Name == "Новеллы (сборник)").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Акутагава").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 25,
                             BookId = Books.FirstOrDefault(x => x.Name == "Заговор. Святая Елена, маленький остров. Повести (сборник)").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Алданов").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 26,
                             BookId = Books.FirstOrDefault(x => x.Name == "Домовёнок Кузька (сборник)").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Александрова").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 27,
                             BookId = Books.FirstOrDefault(x => x.Name == "У войны не женское лицо").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Алексиевич").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 28,
                             BookId = Books.FirstOrDefault(x => x.Name == "Большой Мольн").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Фурнье").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });

                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 29,
                             BookId = Books.FirstOrDefault(x => x.Name == "Маргарита Алигер. Избранное").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Алигер").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });

                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 30,
                             BookId = Books.FirstOrDefault(x => x.Name == "Генералы песчаных карьеров").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Амаду").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                bookToAuthors.Add(
                         new BookToAuthor
                         {
                             Id = 31,
                             BookId = Books.FirstOrDefault(x => x.Name == "Дата Туташхиа").Id,
                             AuthorId = Authors.FirstOrDefault(x => x.LastName == "Амирэджиби").Id,
                             AddUserId = SD.UserIdForInitialData,
                             AddTime = DateTime.Now,
                         });
                return bookToAuthors;
            }
        }
    }
}
