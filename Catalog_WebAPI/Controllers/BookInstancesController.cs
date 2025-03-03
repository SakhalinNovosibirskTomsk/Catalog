using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
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
        /// Получить список всех экземпляров книг выданых читателю
        /// </summary>
        /// <returns>Возвращает все экземпляры книг выданые читателям - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляров книг выданых читателю не найдено</response>
        [HttpGet("AllIsCheckedOut")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsCheckedOutAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsCheckedOut, true);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книг выданых читателю не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }

        /// <summary>
        /// Получить список всех экземпляров книг НЕ выданых читателю
        /// </summary>
        /// <returns>Возвращает все экземпляры книг, которые в данный момент не выданы читателям - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляров книг не выданых читателю не найдено</response>
        [HttpGet("AllIsNotCheckedOut")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsNotCheckedOutAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsCheckedOut, false);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книг не выданых в данный момент читателю не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }


        /// <summary>
        /// Получить список всех экземпляров книг забронированных на данный момент читателями
        /// </summary>
        /// <returns>Возвращает все экземпляры книг забронированные на данный момент читателями - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляров книг забронированных на данный момент читателями не найдено</response>
        [HttpGet("AllIsBooked")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsBookedAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsBooked, true);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книг забронированных на данный момент читателями не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }

        /// <summary>
        /// Получить список всех экземпляров книг не забронированных на данный момент читателями
        /// </summary>
        /// <returns>Возвращает все экземпляры книг, которые не забронированы на данный момент читателями - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Экземпляров книг не забронированных на данный момент читателями не найдено</response>
        [HttpGet("AllIsNotBooked")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsNotBookedAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsBooked, false);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Экземпляров книг не забронированных на данный момент читателями не найдено");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }




        /// <summary>
        /// Получить все списанные экземпляры книг
        /// </summary>
        /// <returns>Возвращает все списанные экземпляры книг - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Списанные экземпляры книг не найдены</response>
        [HttpGet("AllIsWroteOff")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsWroteOffAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsWroteOff, true);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Списанные экземпляры книг не найдены");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }

        /// <summary>
        /// Получить все НЕ списанные экземпляры книг
        /// </summary>
        /// <returns>Возвращает все НЕ списанные экземпляры книг - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не списанные экземпляры книг не найдены</response>
        [HttpGet("AllIsNotWroteOff")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsNotWroteOffAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsWroteOff, false);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Не списанные экземпляры книг не найдены");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }

        /// <summary>
        /// Получить все свободные экземпляры книг (одновременно не списаны, не выданы, не забронированны)
        /// </summary>
        /// <returns>Все свободные экземпляры книг (не списаны, не выданы, не забронированны) - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Свободные экземпляры книг (одновременно не списаны, не выданы, не забронированны) не найдены</response>
        [HttpGet("AllIsFree")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsFreeAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsFree);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Свободные экземпляры книг (одновременно не списаны, не выданы, не забронированны) не найдены");
            return Ok(_mapper.Map<IEnumerable<BookInstance>, IEnumerable<BookInstanceResponse>>(gotBookInstances));
        }


        /// <summary>
        /// Получить все занятые экземпляры книг (или списаны, или выданы, или забронированны)
        /// </summary>
        /// <returns>Все занятые экземпляры книг (или списаны, или выданы, или забронированны) - список объектов типа BookInstanceResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Занятые экземпляры книг (или списаны, или выданы, или забронированны) не найдены</response>
        [HttpGet("AllIsBusy")]
        [ProducesResponseType(typeof(List<BookInstanceResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<BookInstanceResponse>>> GetAllBookInstancesIsBusyAsync()
        {
            var gotBookInstances = await _bookInstanceRepository.GetAllBookInstancesByFlagsAsync(SD.BookInstancesFags.IsBusy);
            if (gotBookInstances == null || gotBookInstances.Count() <= 0)
                return NotFound("Занятые экземпляры книг (или списаны, или выданы, или забронированны) не найдены");
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
        /// Получить данные о нахождении экземпляра книги у читателя или в библиотеке
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные об отметке выдачи экземпляра книги читателю - объект BookInstanceIsCheckedOutResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpGet("GetIsCheckedOut/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsCheckedOutResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsCheckedOutResponse>> GetIsCheckedOut(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceIsCheckedOutResponse>(bookInstance));
        }

        /// <summary>
        /// Установить отметку, что экземпляр книги выдан читателю из библиотеки
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные об отметке выдачи экземпляра книги читателю - объект BookInstanceIsCheckedOutResponse</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Экземпляр книги уже выдан читателю</response>  
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("SetIsCheckedOut/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsCheckedOutResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsCheckedOutResponse>> SetIsCheckedOut(int id)
        {
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");
            if (foundBookInstance.IsCheckedOut == true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже выдан читателю.");
            foundBookInstance.IsCheckedOut = true;
            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceIsCheckedOutResponse>(updatedBookInstance));
        }

        /// <summary>
        /// Снять отметку, что экземпляр книги выдан читателю из библиотеки
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные об отметке выдачи экземпляра книги читателю - объект BookInstanceIsCheckedOutResponse</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Экземпляр книги уже находится в библиотеке</response>  
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("UnSetIsCheckedOut/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsCheckedOutResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsCheckedOutResponse>> UnSetIsCheckedOut(int id)
        {
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");
            if (foundBookInstance.IsCheckedOut != true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже находится в библиотеке.");
            foundBookInstance.IsCheckedOut = false;
            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceIsCheckedOutResponse>(updatedBookInstance));
        }



        /// <summary>
        /// Получить данные о списании экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные о списании экземпляра книги - объект BookInstanceIsWroteOffResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpGet("GetIsWroteOff/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsWroteOffResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsWroteOffResponse>> GetIsWroteOff(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceIsWroteOffResponse>(bookInstance));
        }


        /// <summary>
        /// Установить отметку, что экземпляр книги списан
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные об отметке списания экземпляра книги  - объект BookInstanceIsWroteOffResponse</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Экземпляр книги уже списан</response>  
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("SetIsWroteOff/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsWroteOffResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsWroteOffResponse>> SetIsWroteOff(int id)
        {
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");
            if (foundBookInstance.IsWroteOff == true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже списан.");
            foundBookInstance.IsWroteOff = true;
            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceIsWroteOffResponse>(updatedBookInstance));
        }

        /// <summary>
        /// Снять отметку о списании экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные о списании экземпляра книги - объект BookInstanceIsWroteOffResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("UnSetIsWroteOff/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsWroteOffResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsWroteOffResponse>> UnSetIsWroteOff(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            if (bookInstance.IsWroteOff != true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже НЕ списан.");

            bookInstance.IsWroteOff = false;

            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(bookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceIsWroteOffResponse>(bookInstance));
        }




        /// <summary>
        /// Получить состояение отметки о бронировании экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные о состоянии отметки о бронировании экземпляра книги - объект BookInstanceIsBookedResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpGet("GetIsBookedOff/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsBookedResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsBookedResponse>> GetIsBooked(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceIsBookedResponse>(bookInstance));
        }


        /// <summary>
        /// Установить отметку, что экземпляр книги забронирован
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные о текущем состоянии отметки бронировании экземпляра книги  - объект BookInstanceIsBookedResponse</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Экземпляр книги уже отмечен как забронированный</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("SetIsBooked/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsBookedResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsBookedResponse>> SetIsBooked(int id)
        {
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");
            if (foundBookInstance.IsBooked == true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже отмечен как забронированный.");
            foundBookInstance.IsBooked = true;
            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceIsBookedResponse>(updatedBookInstance));
        }

        /// <summary>
        /// Снять отметку о бронировании экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные о состоянии отметки о бронировании экземпляра книги - объект BookInstanceIsBookedResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("UnSetIsBooked/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceIsBookedResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceIsBookedResponse>> UnSetIsBooked(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            if (bookInstance.IsBooked != true)
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже и так отмечен как НЕ забронированный.");

            bookInstance.IsBooked = false;

            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(bookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceIsBookedResponse>(bookInstance));
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
