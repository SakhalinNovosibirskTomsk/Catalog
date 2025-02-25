using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels.Book;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog_WebAPI.Controllers
{

    /// <summary>
    /// Издатели
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookToAuthorRepository _bookToAuthorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksController(IPublisherRepository publisherRepository,
            IAuthorRepository authorRepository,
            IBookToAuthorRepository bookToAuthorRepository,
            IBookRepository bookRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _bookToAuthorRepository = bookToAuthorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;

        }

        /// <summary>
        /// Получить список всех книг
        /// </summary>
        /// <returns>Возвращает список всех книг - объекты типа BookItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<BookItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BookItemResponse>>> GetAllBooksAsync()
        {
            var gotBooks = await _bookRepository.GetAllBooksAsync(SD.GetAllItems.All);
            var retVar = _mapper.Map<IEnumerable<Book>, IEnumerable<BookItemResponse>>(gotBooks);
            return Ok(retVar);
        }

        /// <summary>
        /// Получить список всех книг находящихся в архиве
        /// </summary>
        /// <returns>Возвращает все книги находящиеся в архиве - список объектов типа BookItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllInArchive")]
        [ProducesResponseType(typeof(List<BookItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BookItemResponse>>> GetAllBooksInArchiveAsync()
        {
            var gotBooks = await _bookRepository.GetAllBooksAsync(SD.GetAllItems.ArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<Book>, IEnumerable<BookItemResponse>>(gotBooks));
        }

        /// <summary>
        /// Получить список всех книг находящихся НЕ в архиве
        /// </summary>
        /// <returns>Возвращает все книги находящиеся НЕ в архиве - список объектов типа BookItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllNotInArchive")]
        [ProducesResponseType(typeof(List<BookItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BookItemResponse>>> GetAllBooksNotInArchiveAsync()
        {
            var gotBooks = await _bookRepository.GetAllBooksAsync(SD.GetAllItems.NotArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<Book>, IEnumerable<BookItemResponse>>(gotBooks));
        }

        /// <summary>
        /// Получить книгу по ИД
        /// </summary>
        /// <param name="id">ИД книги</param>
        /// <returns>Возвращает найденую по ИД книгу - объект типа BookItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Книга с заданным Id не найдена</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookItemResponse>> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
                return NotFound("Книга с ID = " + id.ToString() + " не найдена!");

            return Ok(_mapper.Map<Book, BookItemResponse>(book));
        }

        /// <summary>
        /// Получить книгу по наименованию
        /// </summary>
        /// <param name="name">Наименование книги</param>
        /// <returns>Возвращает найденую по наименованию книгу - объект типа BookItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Книга с заданным наименованием не найден</response>
        [HttpGet("GetByName/{name}")]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookItemResponse>> GetBookByNameAsync(string name)
        {
            var book = await _bookRepository.GetBookByNameAsync(name);

            if (book == null)
                return NotFound("Книга с наименованием \"" + name + "\" не найдена!");

            return Ok(_mapper.Map<Book, BookItemResponse>(book));
        }




        /// <summary>
        /// Создание новой книги
        /// </summary>
        /// <param name="request">Параметры создаваемой книги - объект типа BookItemCreateUpdateRequest</param>
        /// <returns>Возвращает созданую книгу - объект типа BookItemResponse</returns>
        /// <response code="201">Успешное выполнение. Издатель создан</response>
        /// <response code="400">Не удалось добавить книгу. Причина описана в ответе</response>  
        [HttpPost]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BookItemResponse>> CreateBookAsync(BookItemCreateUpdateRequest request)
        {
            var bookFoundByName = await _bookRepository.GetBookByNameAsync(request.Name);
            if (bookFoundByName != null)
                return BadRequest("Уже есть книга с наименованием \"" + request.Name + "\" (ИД = " + bookFoundByName.Id.ToString() + "). Двух книг с одинаковым наименованием быть не может.");

            if (String.IsNullOrWhiteSpace(request.PublisherName))
            {
                return BadRequest("Наименование издателя пустое");
            }

            var foundPublisherByName = await _publisherRepository.GetPublisherByNameAsync(request.PublisherName);

            if (foundPublisherByName == null)
                return BadRequest("Издатель с наименованием \"" + request.PublisherName + "\" не найден в справочнике издателей.");

            if (request.BookAuthors == null || request.BookAuthors.Count <= 0)
                return BadRequest("У книги не указан автор.");

            List<Author> authorsFoundByNameList = new List<Author>();
            foreach (var bookAuthor in request.BookAuthors)
            {
                var author = await _authorRepository.GetAuthorByFullNameAsync(firstName: bookAuthor.FirstName, lastName: bookAuthor.LastName, middleName: bookAuthor.MiddleName);

                if (author == null)
                {
                    return BadRequest("Автор \""
                        + (String.IsNullOrWhiteSpace(bookAuthor.FirstName) ? "" : bookAuthor.FirstName + " ")
                        + (String.IsNullOrWhiteSpace(bookAuthor.LastName) ? "" : bookAuthor.LastName + " ")
                        + (String.IsNullOrWhiteSpace(bookAuthor.MiddleName) ? "" : bookAuthor.MiddleName)
                        + "\" не найден в справочнике авторов.");
                }

                authorsFoundByNameList.Add(author);
            }

            var newBook =
                new Book
                {
                    Name = request.Name,
                    ISBN = request.ISBN,
                    PublisherId = foundPublisherByName.Id,
                    PublishDate = request.PublishDate,
                    EBookLink = "",
                    EBookDownloadCount = 0,
                    AddUserId = SD.UserIdForInitialData,
                    AddTime = DateTime.Now,
                    IsArchive = false,
                };

            var addedBook = await _bookRepository.AddBookAsync(newBook, authorsFoundByNameList);

            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedBook.Id.ToString();
            }
            return Created(routVar, _mapper.Map<Book, BookItemResponse>(addedBook));
        }

        /// <summary>
        /// Изменение существующей книги
        /// </summary>
        /// <param name="id">ИД изменяемоой книги</param>
        /// <param name="request">Новые данные изменяемой книги - объект типа BookItemCreateUpdateRequest</param>
        /// <returns>Возвращает данные изменённой книги - объект BookItemResponse</returns>
        /// <response code="200">Успешное выполнение. Данные книги изменёны</response>
        /// <response code="400">Не удалось изменить данные книги. Причина описана в ответе</response>  
        /// <response code="404">Не удалось найти книгу с указаным ИД</response>  
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookItemResponse>> EditBookAsync(int id, BookItemCreateUpdateRequest request)
        {
            var foundBook = await _bookRepository.GetByIdAsync(id);
            if (foundBook == null)
                return NotFound("Книга с Id = " + id.ToString() + " не найдена.");

            var foundBookByName = await _bookRepository.GetBookByNameAsync(request.Name);

            if (foundBookByName != null)
                if (foundBookByName.Id != foundBook.Id)
                    return BadRequest("Уже есть книга с наименованием = " + request.Name + " (ИД = " + foundBookByName.Id.ToString() + ")");

            if (String.IsNullOrWhiteSpace(request.PublisherName))
                return BadRequest("Не указано наименование издателя в данных изменяемой книги");

            if (request.BookAuthors == null || request.BookAuthors.Count() <= 0)
                return BadRequest("Не указан ни один автор в данных изменяемой книги");

            var foundPublisherByName = await _publisherRepository.GetPublisherByNameAsync(request.PublisherName);

            if (foundPublisherByName == null)
                return BadRequest("Издатель с наименованием \"" + request.PublisherName + "\" не найден в справочнике издателей.");

            List<Author> authorsFoundByNameList = new List<Author>();
            foreach (var bookAuthor in request.BookAuthors)
            {
                var author = await _authorRepository.GetAuthorByFullNameAsync(firstName: bookAuthor.FirstName, lastName: bookAuthor.LastName, middleName: bookAuthor.MiddleName);

                if (author == null)
                {
                    return BadRequest("Автор \""
                        + (String.IsNullOrWhiteSpace(bookAuthor.FirstName) ? "" : bookAuthor.FirstName)
                        + (String.IsNullOrWhiteSpace(bookAuthor.LastName) ? "" : bookAuthor.LastName)
                        + (String.IsNullOrWhiteSpace(bookAuthor.MiddleName) ? "" : bookAuthor.MiddleName)
                        + "\" не найден в справочнике авторов.");
                }

                authorsFoundByNameList.Add(author);
            }


            if (foundBook.Name.Trim().ToUpper() != request.Name.Trim().ToUpper())
                foundBook.Name = request.Name;

            if (foundBook.ISBN.Trim().ToUpper() != request.ISBN.Trim().ToUpper())
                foundBook.ISBN = request.ISBN;

            if (foundBook.PublisherId != foundPublisherByName.Id)
                foundBook.PublisherId = foundPublisherByName.Id;

            if (foundBook.PublishDate != request.PublishDate)
                foundBook.PublishDate = request.PublishDate;

            var updatedBook = await _bookRepository.UpdateBookAsync(foundBook, authorsFoundByNameList);
            return Ok(_mapper.Map<Book, BookItemResponse>(updatedBook));
        }



        /// <summary>
        /// Удалить книгу с указаным ИД в архив
        /// </summary>
        /// <param name="id">ИД удалякмой в архив книги</param>
        /// <returns>Возвращает данные удалённой в архив книги - объект BookItemResponse</returns>
        /// <response code="200">Успешное выполнение. Книга удалёна в архив</response>
        /// <response code="400">Не удалось удалить книгу в архив, так как книга уже в архиве</response>  
        /// <response code="404">Не удалось найти книгу с указаным ИД</response>  
        [HttpPut("DeleteToArchive/{id:int}")]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookItemResponse>> DeleteBookToArchiveAsync(int id)
        {
            var foundBook = await _bookRepository.GetBookByIdAsync(id);
            if (foundBook == null)
                return NotFound("Книга с Id = " + id.ToString() + " не найдена.");
            if (foundBook.IsArchive == true)
                return BadRequest("Книга с Id = " + id.ToString() + " уже в архиве.");
            foundBook.IsArchive = true;
            var updatedBook = await _bookRepository.UpdateAsync(foundBook);
            return Ok(_mapper.Map<Book, BookItemResponse>(updatedBook));
        }


        /// <summary>
        /// Восстановить книгу с указаным ИД из архива
        /// </summary>
        /// <param name="id">ИД восстанавливаемой из архива книги</param>
        /// <returns>Возвращает данные восстановленной из архива книги - объект BookItemResponse</returns>
        /// <response code="200">Успешное выполнение. Книга восстановлена из архива</response>
        /// <response code="400">Не удалось восстановить книгу из архива, так как книга уже не в архиве</response>  
        /// <response code="404">Не удалось найти книгу с указаным ИД</response>  
        [HttpPut("RestoreFromArchive/{id:int}")]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookItemResponse>> RestoreBBookFromArchiveAsync(int id)
        {
            var foundBook = await _bookRepository.GetBookByIdAsync(id);
            if (foundBook == null)
                return NotFound("Книга с Id = " + id.ToString() + " не найдена.");
            if (foundBook.IsArchive != true)
                return BadRequest("Книга с Id = " + id.ToString() + " не находится в архиве. Невозможно восстановить её из архива");
            foundBook.IsArchive = false;
            var updatedBook = await _bookRepository.UpdateAsync(foundBook);
            return Ok(_mapper.Map<Book, BookItemResponse>(updatedBook));
        }



        /// <summary>
        /// Загрузить файл электронной копии книги
        /// </summary>
        /// <param name="id">ИД книги</param>
        /// <param name="file">Файл эленктронной копии книги</param>
        /// <returns>Возвращает изменённый объект BookItemResponse</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Проблема при загрузке файлаю Подробности в сроке ответа</response>  
        /// <response code="404">Не удалось найти книгу с указаным ИД</response>  
        [DisableRequestSizeLimit]
        [HttpPut("UploadFile/{id:int}")]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookItemResponse>> UploadFileAsync(int id, IFormFile file)
        {

            if (file != null && file.Length > 0)
            {

                var foundBook = await _bookRepository.GetBookByIdAsync(id);

                if (foundBook == null)
                    return NotFound("Книга с Id = " + id.ToString() + " не найдена.");

                if (!String.IsNullOrWhiteSpace(foundBook.EBookLink))
                {
                    if (System.IO.File.Exists(foundBook.EBookLink))
                    {
                        System.IO.File.Delete(foundBook.EBookLink);
                    }
                }

                var extension = Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(SD.BookECopyPath, Guid.NewGuid().ToString() + extension);
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    file.CopyTo(fileStream);
                    if (fileStream != null)
                        await fileStream.DisposeAsync();
                }

                if (System.IO.File.Exists(fullPath))
                {
                    foundBook.EBookLink = fullPath;
                    var updatedBook = await _bookRepository.UpdateAsync(foundBook);

                    return Ok(_mapper.Map<Book, BookItemResponse>(updatedBook));
                }
                else
                {
                    return BadRequest("Не удалось найти файл по пути \": " + fullPath + "\". Объект книги не изменён.");
                }
            }
            else
            {
                return BadRequest("Файл не выбран или пустой!");
            }
        }


        /// <summary>
        /// Удалить файл электронной копии книги
        /// </summary>
        /// <param name="id">ИД книги</param>        
        /// <returns>Возвращает изменённый объект BookItemResponse</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">У книги нет ссылки на файл электронной копии</response>  
        /// <response code="404">Книга с указанным или файл не найдены</response>  
        [DisableRequestSizeLimit]
        [HttpDelete("DeleteFile/{id:int}")]
        [ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookItemResponse>> DeleteFileAsync(int id)
        {

            var foundBook = await _bookRepository.GetBookByIdAsync(id);

            if (foundBook == null)
                return NotFound("Книга с Id = " + id.ToString() + " не найдена.");

            if (!String.IsNullOrWhiteSpace(foundBook.EBookLink))
            {
                if (System.IO.File.Exists(foundBook.EBookLink))
                {
                    System.IO.File.Delete(foundBook.EBookLink);
                    foundBook.EBookLink = "";
                    var updatedBook = await _bookRepository.UpdateAsync(foundBook);

                    return Ok(_mapper.Map<Book, BookItemResponse>(updatedBook));
                }
                else
                {
                    return NotFound("Файл не найден: \"" + foundBook.EBookLink + "\"");
                }
            }
            else
            {
                return BadRequest("У книги нет ссылки на файл электронной копии");
            }
        }



        /// <summary>
        /// Скачать файл электронной копии книги
        /// </summary>
        /// <param name="id">ИД книги</param>
        /// <returns>Возвращает файл эдектронной копии книги</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">У книги нет ссылки на файл электронной копии</response>  
        /// <response code="404">Книга с указанным или файл не найдены</response>  
        [DisableRequestSizeLimit]
        [HttpGet("DownloadFile/{id}")]
        [ProducesResponseType(typeof(File), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DownloadFileAsync(int id)
        {

            var foundBook = await _bookRepository.GetBookByIdAsync(id);

            if (foundBook == null)
                return NotFound("Книга с Id = " + id.ToString() + " не найдена.");

            if (!String.IsNullOrWhiteSpace(foundBook.EBookLink))
            {
                if (System.IO.File.Exists(foundBook.EBookLink))
                {

                    var forFileName = "Book_id_" + id.ToString();
                    return Ok(File(new FileStream(foundBook.EBookLink, FileMode.Open), "application/pdf", forFileName));
                    //return Ok(File(new FileStream(foundBook.EBookLink, FileMode.Open), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", forFileName));

                }
                else
                {
                    return NotFound("Файл не найден: \"" + foundBook.EBookLink + "\"");
                }
            }
            else
            {
                return BadRequest("У книги нет ссылки на файл электронной копии");
            }
        }
    }

}
