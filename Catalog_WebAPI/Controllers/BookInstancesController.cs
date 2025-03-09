using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Domain.CatalogDB;
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
        private readonly IBookInstanceRepository _bookInstanceRepository;
        private readonly IMapper _mapper;

        public BookInstancesController(IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IBookInstanceRepository bookInstanceRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
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
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesAsync();
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Не найдено ни одного экземпляра книг");
            var retVar = _mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances);
            return Ok(retVar);
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
        /// Получить экземпляр книги по ИД
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
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

        /// <summary>
        /// Получить максимальное количество дней, на которые можно выдавать читателю экземпляр книги по ИД экземпляра
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает найденый экземпляр книги по ИД - объект типа BookInstanceOutMaxDaysResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляр книги с заданным ИД не найден</response>
        [HttpGet("GetBookInstanceOutMaxDays/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceOutMaxDaysResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceOutMaxDaysResponse>> GetBookInstanceOutMaxDaysAsync(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceOutMaxDaysResponse>(bookInstance));
        }

        /// <summary>
        /// Получить отметку о выдаче экземпляра книги только в читальный зал по ИД экземпляра
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает найденый экземпляр книги по ИД - объект типа BookInstanceOnlyForReadingRoomResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляр книги с заданным ИД не найден</response>
        [HttpGet("GetBookInstanceOnlyForReadingRoom/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceOutMaxDaysResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceOutMaxDaysResponse>> GetBookInstanceOnlyForReadingRoomAsync(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceOnlyForReadingRoomResponse>(bookInstance));
        }


        /// <summary>
        /// Создание нового экземпляра книги
        /// </summary>
        /// <param name="request">Параметры создаваемого экземпляра книги - объект типа BookInstanceCreateUpdateRequest</param>
        /// <returns>Возвращает созданый объект экземпляра книги - объект типа BookInstanceResponse</returns>
        /// <response code="201">Успешное выполнение. Экземпляр книги создан</response>
        /// <response code="400">Не удалось добавить экземпляр книги. Причина описана в ответе</response>  
        /// <response code="400">Не найдена книга, по которой создаётся экземпляр, статус книги в справочнике статусов. Причина описана в ответе</response>  
        [HttpPost]
        [ProducesResponseType(typeof(BookInstanceResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceResponse>> CreateBookInstanceAsync(BookInstanceCreateUpdateRequest request)
        {
            var foundBookById = await _bookRepository.GetBookByIdAsync(request.BookId);
            if (foundBookById == null)
                return NotFound("Не найдена книга с ИД = " + request.BookId.ToString() + " в справочнике книг");

            var foundBookInstanceByInventoryNumber = await _bookInstanceRepository.GetBookInstanceByInventoryNumberAsync(request.InventoryNumber);
            if (foundBookInstanceByInventoryNumber != null)
            {
                return BadRequest("Уже есть экземпляр книги с InventoryNumber = " + request.InventoryNumber + " (ИД экземпляра книги = " + foundBookInstanceByInventoryNumber.Id.ToString() + ")");
            }

            if (request.OutMaxDays < 0)
                return BadRequest("Поле OutMaxDays не может быть отрицательным");

            var newBookInstance = _mapper.Map<BookInstanceCreateUpdateRequest, BookInstance>(request);

            var addedBookInstance = await _bookInstanceRepository.AddAsync(newBookInstance);

            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedBookInstance.Id.ToString();
            }
            return Created(routVar, _mapper.Map<BookInstance, BookInstanceResponse>(addedBookInstance));
        }

        /// <summary>
        /// Изменение существующего экземпляра книги
        /// </summary>
        /// <param name="id">ИД изменяемого экземпляра книги</param>
        /// <param name="request">Новые данные изменяемого экземпляра книги - объект типа BookInstanceCreateUpdateRequest</param>
        /// <returns>Возвращает данные изменённого экземпляра книги - объект BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение. Данные экземпляра книги изменёны</response>
        /// <response code="400">Не удалось изменить данные экземпляра книги. Причина описана в ответе</response>  
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД, книгу, на которорый он ссылается, статус. Подробности в строке ответа</response>  
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BookInstanceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceResponse>> EditBookInstanceAsync(int id, BookInstanceCreateUpdateRequest request)
        {
            var foundBookInstanceById = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstanceById == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");

            var foundBookById = await _bookRepository.GetBookByIdAsync(request.BookId);

            if (foundBookById == null)
                return NotFound("Не найдена книга с ИД = " + request.BookId.ToString());

            var foundBookInstanceByInventoryNumber = await _bookInstanceRepository.GetBookInstanceByInventoryNumberAsync(request.InventoryNumber);
            if (foundBookInstanceByInventoryNumber != null)
                if (foundBookInstanceByInventoryNumber.Id != id)
                    return BadRequest("Уже есть эезнемпляр книги с InventoryNumber = " + request.InventoryNumber +
                        " (ИД экземпляра книги = " + foundBookInstanceByInventoryNumber.Id.ToString());


            if (request.OutMaxDays < 0)
                return BadRequest("Поле OutMaxDays не может быть отрицательным");

            if (request.BookId != foundBookInstanceById.BookId)
                foundBookInstanceById.BookId = request.BookId;

            if (request.InventoryNumber.Trim().ToUpper() != foundBookInstanceById.InventoryNumber.Trim().ToUpper())
                foundBookInstanceById.InventoryNumber = request.InventoryNumber;

            if (request.OnlyForReadingRoom != foundBookInstanceById.OnlyForReadingRoom)
                foundBookInstanceById.OnlyForReadingRoom = request.OnlyForReadingRoom;

            if (request.OutMaxDays != foundBookInstanceById.OutMaxDays)
                foundBookInstanceById.OutMaxDays = request.OutMaxDays;

            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstanceById);
            return Ok(_mapper.Map<BookInstance, BookInstanceResponse>(updatedBookInstance));
        }

        /// <summary>
        /// Удалить экземпляр книги
        /// </summary>
        /// <param name="id">ИД удаляемой записи</param>
        /// <returns>Возвращает количество удалённых записей</returns>
        /// <response code="200">Успешное выполнение.</response>        
        /// <response code="404">Не удалось найти эуземпляр книги с указанным ИД</response>  
        [HttpDelete("DeleteById/{id:int}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<int>> DeleteBookInstanceByIdAsync(int id)
        {

            // TODO Или убрать удаление, или перед удалением проверять на наличие ссылок в БД других сервисов
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Запись с ИД = " + id.ToString() + " не найдена.");

            return Ok(await _bookInstanceRepository.DeleteAsync(foundBookInstance));
        }
    }

}
