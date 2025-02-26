using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels.BookInstance;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog_WebAPI.Controllers
{

    /// <summary>
    /// Экземпляры книг
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookInstancesController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IBookInstanceRepository _bookInstanceRepository;
        private readonly IMapper _mapper;

        public BookInstancesController(IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IStateRepository stateRepository,
            IBookInstanceRepository bookInstanceRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _stateRepository = stateRepository;
            _bookInstanceRepository = bookInstanceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех экземпляров книг
        /// </summary>
        /// <returns>Возвращает список всех экземпляров книг - объекты типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не найдено ни одного экземпляра книг</response>
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesAsync(SD.GetAllItems.All);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Не найдено ни одного экземпляра книг");
            var retVar = _mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances);
            return Ok(retVar);
        }

        /// <summary>
        /// Получить список всех экземпляров книг находящихся в архиве
        /// </summary>
        /// <returns>Возвращает все экземпляры книг находящиеся в архиве - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляров книг находящихся в архиве не найдено</response>
        [HttpGet("AllInArchive")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesInArchiveAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesAsync(SD.GetAllItems.ArchiveOnly);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книг находящихся в архиве не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }

        /// <summary>
        /// Получить список всех экземпляров книг находящихся НЕ в архиве
        /// </summary>
        /// <returns>Возвращает все экземпляры книг находящиеся НЕ в архиве - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляров книг НЕ в архиве не найдено</response>
        [HttpGet("AllNotInArchive")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesNotInArchiveAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesAsync(SD.GetAllItems.NotArchiveOnly);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книг НЕ в архиве не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }

        /// <summary>
        /// Получить список всех экземпляров по ИД книги
        /// </summary>
        /// <param name="bookId">ИД книги</param>
        /// <returns>Возвращает все экземпляры книги с указанным ИД книги - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляры книги с заданным ИД книги не найдены</response>
        [HttpGet("GetBookInstancesByBookId/{bookId}")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetBookInstancesByBookIdAsync(int bookId)
        {
            var gotBookInstances = await _bookInstanceRepository.GetBookInstancesByBookIdAsync(bookId);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книги с ИД книги = " + bookId.ToString() + " не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }

        /// <summary>
        /// Получить список всех экземпляров по ИД статуса состояния экземпляра
        /// </summary>
        /// <param name="stateId">ИД статуса состояния экземпляра</param>
        /// <returns>Возвращает все экземпляры книги с указанным ИД статуса состояния экземпляра - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляры книги с заданным ИД статуса состояния экземпляра не найдены</response>
        [HttpGet("GetBookInstancesByStateId/{stateId}")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetBookInstancesByStateIdAsync(int stateId)
        {
            var gotBookInstances = await _bookInstanceRepository.GetBookInstancesByStateIdAsync(stateId);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляры книги с заданным ИД статуса состояния экземпляра = " + stateId.ToString() + " не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }


        /// <summary>
        /// Получить список всех экземпляров по ИД книги и ИД статуса состояния экземпляра
        /// </summary>
        /// <param name="bookId">ИД книги</param>
        /// <param name="stateId">ИД статуса состояния экземпляра</param>
        /// <returns>Возвращает все экземпляры книги с указанными ИД книги и ИД статуса состояния экземпляра - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляры книги с заданными ИД книги и ИД статуса состояния экземпляра не найдены</response>
        [HttpGet("GetBookInstancesByBookIdAndStateId/{bookId}/{stateId}")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetBookInstancesByBookIdAndStateIdAsync(int bookId, int stateId)
        {
            var gotBookInstances = await _bookInstanceRepository.GetBookInstancesByBookIdAndStateIdAsync(bookId, stateId);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книги с заданными ИД книги = " + bookId.ToString() + " и ИД статуса состояния экземпляра = " + stateId.ToString() + " не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }



        /// <summary>
        /// Получить экземпляр книги по ИД
        /// </summary>
        /// <param name="id">ИД книги</param>
        /// <returns>Возвращает найденый экземпляр книги по ИД - объект типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляр книги с заданным ИД не найден</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BookInstanceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceResponse>> GetBookInstanceByIdAsync(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceResponse>(bookInstance));
        }




        ///// <summary>
        ///// Создание новой книги
        ///// </summary>
        ///// <param name="request">Параметры создаваемой книги - объект типа BookItemCreateUpdateRequest</param>
        ///// <returns>Возвращает созданую книгу - объект типа BookItemResponse</returns>
        ///// <response code="201">Успешное выполнение. Издатель создан</response>
        ///// <response code="400">Не удалось добавить книгу. Причина описана в ответе</response>  
        //[HttpPost]
        //[ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.Created)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<BookItemResponse>> CreateBookAsync(BookItemCreateUpdateRequest request)
        //{
        //    var bookFoundByName = await _bookRepository.GetBookByNameAsync(request.Name);
        //    if (bookFoundByName != null)
        //        return BadRequest("Уже есть книга с наименованием \"" + request.Name + "\" (ИД = " + bookFoundByName.Id.ToString() + "). Двух книг с одинаковым наименованием быть не может.");

        //    if (String.IsNullOrWhiteSpace(request.PublisherName))
        //    {
        //        return BadRequest("Наименование издателя пустое");
        //    }

        //    var foundPublisherByName = await _publisherRepository.GetPublisherByNameAsync(request.PublisherName);

        //    if (foundPublisherByName == null)
        //        return BadRequest("Издатель с наименованием \"" + request.PublisherName + "\" не найден в справочнике издателей.");

        //    if (request.BookAuthors == null || request.BookAuthors.Count <= 0)
        //        return BadRequest("У книги не указан автор.");

        //    List<Author> authorsFoundByNameList = new List<Author>();
        //    foreach (var bookAuthor in request.BookAuthors)
        //    {
        //        var author = await _authorRepository.GetAuthorByFullNameAsync(firstName: bookAuthor.FirstName, lastName: bookAuthor.LastName, middleName: bookAuthor.MiddleName);

        //        if (author == null)
        //        {
        //            return BadRequest("Автор \""
        //                + (String.IsNullOrWhiteSpace(bookAuthor.FirstName) ? "" : bookAuthor.FirstName + " ")
        //                + (String.IsNullOrWhiteSpace(bookAuthor.LastName) ? "" : bookAuthor.LastName + " ")
        //                + (String.IsNullOrWhiteSpace(bookAuthor.MiddleName) ? "" : bookAuthor.MiddleName)
        //                + "\" не найден в справочнике авторов.");
        //        }

        //        authorsFoundByNameList.Add(author);
        //    }

        //    var newBook =
        //        new Book
        //        {
        //            Name = request.Name,
        //            ISBN = request.ISBN,
        //            PublisherId = foundPublisherByName.Id,
        //            PublishDate = request.PublishDate,
        //            EBookLink = "",
        //            EBookDownloadCount = 0,
        //            AddUserId = SD.UserIdForInitialData,
        //            AddTime = DateTime.Now,
        //            IsArchive = false,
        //        };

        //    var addedBook = await _bookRepository.AddBookAsync(newBook, authorsFoundByNameList);

        //    var routVar = "";
        //    if (Request != null)
        //    {
        //        routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
        //            + "/" + addedBook.Id.ToString();
        //    }
        //    return Created(routVar, _mapper.Map<Book, BookItemResponse>(addedBook));
        //}

        ///// <summary>
        ///// Изменение существующей книги
        ///// </summary>
        ///// <param name="id">ИД изменяемоой книги</param>
        ///// <param name="request">Новые данные изменяемой книги - объект типа BookItemCreateUpdateRequest</param>
        ///// <returns>Возвращает данные изменённой книги - объект BookItemResponse</returns>
        ///// <response code="200">Успешное выполнение. Данные книги изменёны</response>
        ///// <response code="400">Не удалось изменить данные книги. Причина описана в ответе</response>  
        ///// <response code="404">Не удалось найти книгу с указаным ИД</response>  
        //[HttpPut("{id:int}")]
        //[ProducesResponseType(typeof(BookItemResponse), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult<BookItemResponse>> EditBookAsync(int id, BookItemCreateUpdateRequest request)
        //{
        //    var foundBook = await _bookRepository.GetByIdAsync(id);
        //    if (foundBook == null)
        //        return NotFound("Книга с Id = " + id.ToString() + " не найдена.");

        //    var foundBookByName = await _bookRepository.GetBookByNameAsync(request.Name);

        //    if (foundBookByName != null)
        //        if (foundBookByName.Id != foundBook.Id)
        //            return BadRequest("Уже есть книга с наименованием = " + request.Name + " (ИД = " + foundBookByName.Id.ToString() + ")");

        //    if (String.IsNullOrWhiteSpace(request.PublisherName))
        //        return BadRequest("Не указано наименование издателя в данных изменяемой книги");

        //    if (request.BookAuthors == null || request.BookAuthors.Count() <= 0)
        //        return BadRequest("Не указан ни один автор в данных изменяемой книги");

        //    var foundPublisherByName = await _publisherRepository.GetPublisherByNameAsync(request.PublisherName);

        //    if (foundPublisherByName == null)
        //        return BadRequest("Издатель с наименованием \"" + request.PublisherName + "\" не найден в справочнике издателей.");

        //    List<Author> authorsFoundByNameList = new List<Author>();
        //    foreach (var bookAuthor in request.BookAuthors)
        //    {
        //        var author = await _authorRepository.GetAuthorByFullNameAsync(firstName: bookAuthor.FirstName, lastName: bookAuthor.LastName, middleName: bookAuthor.MiddleName);

        //        if (author == null)
        //        {
        //            return BadRequest("Автор \""
        //                + (String.IsNullOrWhiteSpace(bookAuthor.FirstName) ? "" : bookAuthor.FirstName)
        //                + (String.IsNullOrWhiteSpace(bookAuthor.LastName) ? "" : bookAuthor.LastName)
        //                + (String.IsNullOrWhiteSpace(bookAuthor.MiddleName) ? "" : bookAuthor.MiddleName)
        //                + "\" не найден в справочнике авторов.");
        //        }

        //        authorsFoundByNameList.Add(author);
        //    }


        //    if (foundBook.Name.Trim().ToUpper() != request.Name.Trim().ToUpper())
        //        foundBook.Name = request.Name;

        //    if (foundBook.ISBN.Trim().ToUpper() != request.ISBN.Trim().ToUpper())
        //        foundBook.ISBN = request.ISBN;

        //    if (foundBook.PublisherId != foundPublisherByName.Id)
        //        foundBook.PublisherId = foundPublisherByName.Id;

        //    if (foundBook.PublishDate != request.PublishDate)
        //        foundBook.PublishDate = request.PublishDate;

        //    var updatedBook = await _bookRepository.UpdateBookAsync(foundBook, authorsFoundByNameList);
        //    return Ok(_mapper.Map<Book, BookItemResponse>(updatedBook));
        //}



        /// <summary>
        /// Удалить экземпляр книги с указаным ИД в архив
        /// </summary>
        /// <param name="id">ИД удаляемого в архив экземпляра книги</param>
        /// <returns>Возвращает данные удалённого в архив экземпляра книги - объект BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение. Экземпляр книги удалён в архив</response>
        /// <response code="400">Не удалось удалить экземпляр книги в архив, так как он уже в архиве</response>  
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("DeleteToArchive/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceResponse>> DeleteBookInstanceToArchiveAsync(int id)
        {
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");
            if (foundBookInstance.IsArchive == true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже в архиве.");
            foundBookInstance.IsArchive = true;
            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceResponse>(updatedBookInstance));
        }


        /// <summary>
        /// Восстановить экземпляр книги с указаным ИД из архива
        /// </summary>
        /// <param name="id">ИД восстанавливаемого из архива экземпляра книги</param>
        /// <returns>Возвращает данные восстановленного из архива экземпляра книги - объект BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение. Экземпляр книги восстановлен из архива</response>
        /// <response code="400">Не удалось восстановить экземпляр книги из архива, так как он уже не в архиве</response>  
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("RestoreFromArchive/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceResponse>> RestoreBookInstanceFromArchiveAsync(int id)
        {
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");
            if (foundBookInstance.IsArchive != true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " не находится в архиве. Невозможно восстановить его из архива");
            foundBookInstance.IsArchive = false;
            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceResponse>(updatedBookInstance));
        }
    }

}
