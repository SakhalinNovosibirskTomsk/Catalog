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
    public class BookToAuthorsController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookToAuthorRepository _bookToAuthorRepository;

        private readonly IMapper _mapper;

        public BookToAuthorsController(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IBookToAuthorRepository bookToAuthorRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookToAuthorRepository = bookToAuthorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список привязанных друг к другу книг и авторов
        /// </summary>
        /// <returns>Возвращает список всех привязок аторов к книгам - объекты типа BookToAuthorResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<BookToAuthorResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<BookToAuthorResponse>>> GetAllBookToAuthorAsyncAsync()
        {
            var gotBookToAuthors = await _bookToAuthorRepository.GetAllBookToAuthorAsync();
            return Ok(_mapper.Map<IEnumerable<BookToAuthor>, IEnumerable<BookToAuthorResponse>>(gotBookToAuthors));
        }


        /// <summary>
        /// Получить привязку автора к книге по записи ИД
        /// </summary>
        /// <param name="id">ИД записи привязки автора к книге</param>
        /// <returns>Возвращает найденую привязку автора к книге - объект типа BookToAuthorResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Запись с заданным Id не найдена</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BookToAuthorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookToAuthorResponse>> GetBookToAuthorByIdAsync(int id)
        {
            var foundBookToAuthor = await _bookToAuthorRepository.GetBookToAuthorByIdAsync(id);

            if (foundBookToAuthor == null)
                return NotFound("Запись с ID = " + id.ToString() + " не найдена!");

            return Ok(_mapper.Map<BookToAuthor, BookToAuthorResponse>(foundBookToAuthor));
        }


        /// <summary>
        /// Найти запись со связкой книги с автором по ИД книги и ИД автора
        /// </summary>
        /// <param name="bookId">ИД книги</param>
        /// <param name="authorId">ИД автора</param>
        /// <returns>Возвращает объект записи связки книги с автором - объект BookToAuthorResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Запись связки книги с автором по ИД книги и ИД автора не найдена</response>
        [HttpGet("GetByBookIdAndAuthorId/{bookId}/{authorId}")]
        [ProducesResponseType(typeof(BookToAuthorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookToAuthorResponse>> GetByBookIdAndAuthorIdAsync(int bookId, int authorId)
        {
            var foundBookToAuthor = await _bookToAuthorRepository.FindBookToAuthorByBookIdAndAuthorIdAsync(bookId, authorId);

            if (foundBookToAuthor == null)
                return NotFound("Связка книги с ИД = " + bookId.ToString() + " с автором с ИД =  " + authorId.ToString() + " не найдена!");

            return Ok(_mapper.Map<BookToAuthor, BookToAuthorResponse>(foundBookToAuthor));
        }

        /// <summary>
        /// Найти все связки книги с авторами по ИД книги
        /// </summary>
        /// <param name="bookId">ИД книги</param>        
        /// <returns>Возвращает список записей связки книги с автором - объекты BookToAuthorResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Записи связки книги с авторами по ИД книги не найдены</response>
        [HttpGet("GetByBookId/{bookId}")]
        [ProducesResponseType(typeof(BookToAuthorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<BookToAuthorResponse>>> GetByBookIdAsync(int bookId)
        {
            var foundBookToAuthorList = await _bookToAuthorRepository.FindBookToAuthorsByBookIdAsync(bookId);

            if (foundBookToAuthorList == null)
                return NotFound("У книги с ИД = " + bookId.ToString() + " не найдено авторов!");

            return Ok(_mapper.Map<IEnumerable<BookToAuthor>, IEnumerable<BookToAuthorResponse>>(foundBookToAuthorList));
        }




        /// <summary>
        /// Создание новой связки книги и автора
        /// </summary>
        /// <param name="bookId">ИД книги</param>  
        /// <param name="authorId">ИД автора</param>  
        /// <returns>Возвращает созданую связку книги и автора - объект типа BookToAuthorResponse</returns>
        /// <response code="201">Успешное выполнение. Связка книги и автора создана</response>
        /// <response code="400">Не удалось добавить издателя. Причина описана в ответе</response>  
        /// <response code="404">Не удалось найти книгу или автора</response>  
        [HttpPost("AddBookToAuthor/{bookId}/{authorId}")]
        [ProducesResponseType(typeof(BookToAuthorResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookToAuthorResponse>> CreateBookToAuthorAsync(int bookId, int authorId)
        {
            var foundBook = await _bookRepository.GetByIdAsync(bookId);
            if (foundBook == null)
                return NotFound("Не найдена книга с ИД = " + bookId.ToString() + " в справочнике книг");

            var foundAuthor = await _authorRepository.GetByIdAsync(authorId);
            if (foundAuthor == null)
                return NotFound("Не найден автор с ИД = " + authorId.ToString() + " в справочнике авторов");

            var foundBookToAuthor = await _bookToAuthorRepository.FindBookToAuthorByBookIdAndAuthorIdAsync(bookId, authorId);
            if (foundBookToAuthor != null)
                return BadRequest("Уже существует связка книги с ИД = " + bookId.ToString() + " с автором с ИД = " + authorId.ToString());

            var addedBookToAuthor = await _bookToAuthorRepository.AddAsync(
                new BookToAuthor
                {
                    BookId = bookId,
                    AuthorId = authorId,
                    // TODO Пользователя добавившего запись в будущем нужно брать реального
                    AddUserId = SD.UserIdForInitialData,
                    AddTime = DateTime.Now,
                });

            addedBookToAuthor = await _bookToAuthorRepository.GetBookToAuthorByIdAsync(addedBookToAuthor.Id);

            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedBookToAuthor.Id.ToString();
            }
            return Created(routVar, _mapper.Map<BookToAuthor, BookToAuthorResponse>(addedBookToAuthor));
        }



        /// <summary>
        /// Удалить связку книги и автора по ИД записи связки
        /// </summary>
        /// <param name="id">ИД удаляемой записи</param>
        /// <returns>Возвращает количество удалённых записей</returns>
        /// <response code="200">Успешное выполнение. Связка книги и автора удалёна</response>        
        /// <response code="404">Не удалось связку книги с авторос с указаным ИД</response>  
        [HttpPut("DeleteById/{id:int}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<int>> DeleteBookToAuthorByIdAsync(int id)
        {
            var foundBookToAuthor = await _bookToAuthorRepository.GetByIdAsync(id);
            if (foundBookToAuthor == null)
                return NotFound("Запись с ИД = " + id.ToString() + " не найдена.");

            return Ok(await _bookToAuthorRepository.DeleteAsync(foundBookToAuthor));
        }

        /// <summary>
        /// Удалить связку книги и автора по ИД книги и ИД автора
        /// </summary>
        /// <param name="bookId">ИД книги</param>
        /// <param name="authorId">ИД автора</param>
        /// <returns>Возвращает количество удалённых записей</returns>
        /// <response code="200">Успешное выполнение. Связка книги и автора удалёна</response>
        /// <response code="404">Не удалось связку книги с авторос с указаным ИД</response>  
        [HttpPut("DeleteByBookIdAndAuthorId/{bookId}/{authorId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<int>> DeleteBookToAuthorByBookIdAndAuthorIdAsync(int bookId, int authorId)
        {
            var foundBookToAuthor = await _bookToAuthorRepository.FindBookToAuthorByBookIdAndAuthorIdAsync(bookId, authorId);
            if (foundBookToAuthor == null)
                return NotFound("Запись с ИД книги = " + bookId.ToString() + " и ИД автора = " + authorId.ToString() + " не найдена.");

            return Ok(await _bookToAuthorRepository.DeleteAsync(foundBookToAuthor));
        }
    }
}
