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

            var foundStateById = await _stateRepository.GetByIdAsync(request.StateId);
            if (foundStateById == null)
                return NotFound("Не найден статус с ИД = " + request.StateId.ToString() + " в справочнике статусов состояний экземпляров книг");

            if (foundStateById.IsNeedComment)
            {
                if (request.FactCommentId == null || request.FactCommentId <= 0)
                    return BadRequest("Статус с ИД = " + request.StateId.ToString() + " (наименование = " + foundStateById.Name + ")"
                        + " требует комментария, но request.FactCommentId пустое. Для статусов, требующих комментария (State.IsNeedComment = true) "
                        + "при создании экземпляра книги с таким статусов необходимо, чтобы поля FactCommentId и FactCommentText были заполнены.");

                if (String.IsNullOrWhiteSpace(request.FactCommentText))
                    return BadRequest("Статус с ИД = " + request.StateId.ToString() + " (наименование = " + foundStateById.Name + ")"
                        + " требует комментария, но request.FactCommentText пустое. Для статусов, требующих комментария (State.IsNeedComment = true) "
                        + "при создании экземпляра книги с таким статусов необходимо, чтобы поля FactCommentId и FactCommentText были заполнены.");
            }

            var foundBookInstanceByInventoryNumber = await _bookInstanceRepository.GetBookInstanceByInventoryNumberAsync(request.InventoryNumber);
            if (foundBookInstanceByInventoryNumber == null)
            {
                return BadRequest("Уже есть экземпляр книги с InventoryNumber = " + request.InventoryNumber + " (ИД экземпляра книги = " + foundBookInstanceByInventoryNumber.Id.ToString() + ")");
            }

            if (request.OutMaxDays < 0)
                return BadRequest("Поле OutMaxDays не может быть отрицательным");

            //var newBookInstance =
            //    new BookInstance
            //    {
            //        BookId = request.BookId,
            //        InventoryNumber = request.InventoryNumber,
            //        ReceiptDate = DateTime.Now,
            //        OnlyForReadingRoom = request.OnlyForReadingRoom,
            //        StateId = request.StateId,
            //        FactCommentId = request.FactCommentId,
            //        FactCommentText = request.FactCommentText,
            //        OutMaxDays = request.OutMaxDays,
            //        AddUserId = SD.UserIdForInitialData,
            //        AddTime = DateTime.Now,
            //        IsArchive = false
            //    };

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

            var foundStateById = await _stateRepository.GetByIdAsync(request.StateId);
            if (foundStateById == null)
                return NotFound("Не найден статус с ИД = " + request.StateId.ToString() + " в справочнике статусов состояний экземпляров книг");

            if (foundStateById.IsNeedComment)
            {
                if (request.FactCommentId == null || request.FactCommentId <= 0)
                    return BadRequest("Статус с ИД = " + request.StateId.ToString() + " (наименование = " + foundStateById.Name + ")"
                        + " требует комментария, но поле FactCommentId пустое. Для статусов, требующих комментария (State.IsNeedComment = true) "
                        + "при создании экземпляра книги с таким статусом необходимо, чтобы поля FactCommentId и FactCommentText были заполнены.");

                if (String.IsNullOrWhiteSpace(request.FactCommentText))
                    return BadRequest("Статус с ИД = " + request.StateId.ToString() + " (наименование = " + foundStateById.Name + ")"
                        + " требует комментария, но поле FactCommentText пустое. Для статусов, требующих комментария (State.IsNeedComment = true) "
                        + "при создании экземпляра книги с таким статусом необходимо, чтобы поля FactCommentId и FactCommentText были заполнены.");
            }

            if (request.OutMaxDays < 0)
                return BadRequest("Поле OutMaxDays не может быть отрицательным");

            if (request.BookId != foundBookInstanceById.BookId)
                foundBookInstanceById.BookId = request.BookId;

            if (request.InventoryNumber.Trim().ToUpper() != foundBookInstanceById.InventoryNumber.Trim().ToUpper())
                foundBookInstanceById.InventoryNumber = request.InventoryNumber;

            if (request.OnlyForReadingRoom != foundBookInstanceById.OnlyForReadingRoom)
                foundBookInstanceById.OnlyForReadingRoom = request.OnlyForReadingRoom;

            if (request.StateId != foundBookInstanceById.StateId)
                foundBookInstanceById.StateId = request.StateId;

            if (request.FactCommentId != foundBookInstanceById.FactCommentId)
                foundBookInstanceById.FactCommentId = request.FactCommentId;

            if (request.FactCommentText != foundBookInstanceById.FactCommentText)
                foundBookInstanceById.FactCommentText = request.FactCommentText;

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
                return BadRequest("Экземпляр книги с ИД = " + id.ToString() + " уже выдан чмитателю.");
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
        /// Получить данные о статусе состояния экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>        
        /// <returns>Возвращает данные статусе состояния экземпляра книги читателю - объект BookInstanceStateResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpGet("GetState/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceStateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceStateResponse>> GetState(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceStateResponse>(bookInstance));
        }

        /// <summary>
        /// Установить статус состояния экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>        
        /// <param name="request">Параметры устанавливаемого статуса экземпляра книги - объект типа BookInstanceUpdateStateRequest</param>
        /// <returns>Возвращает данные об установленном статусе состояния эеземпляра книги - объект BookInstanceStateResponse</returns>
        /// <response code="200">Успешное выполнение.</response>
        /// <response code="400">Статус требует комментарий, но он не указан</response>  
        /// <response code="404">Не найден статус или эеземпляр книги с указаным ИД</response>  
        [HttpPut("SetState/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceStateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceStateResponse>> SetState(int id, BookInstanceUpdateStateRequest request)
        {
            var foundBookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);
            if (foundBookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден.");


            var foundStateById = await _stateRepository.GetByIdAsync(request.StateId);
            if (foundStateById == null)
                return NotFound("Не найден статус с ИД = " + request.StateId.ToString() + " в справочнике статусов состояний экземпляров книг");

            if (foundStateById.IsNeedComment)
            {
                if (request.FactCommentId == null || request.FactCommentId <= 0)
                    return BadRequest("Статус с ИД = " + request.StateId.ToString() + " (наименование = " + foundStateById.Name + ")"
                        + " требует комментария, но поле FactCommentId пустое. Для статусов, требующих комментария (State.IsNeedComment = true) "
                        + "необходимо, чтобы поля FactCommentId и FactCommentText были заполнены.");

                if (String.IsNullOrWhiteSpace(request.FactCommentText))
                    return BadRequest("Статус с ИД = " + request.StateId.ToString() + " (наименование = " + foundStateById.Name + ")"
                        + " требует комментария, но поле FactCommentText в запросе пустое. Для статусов, требующих комментария (State.IsNeedComment = true) "
                        + "необходимо, чтобы поля FactCommentId и FactCommentText были заполнены.");
            }

            if (foundBookInstance.StateId != request.StateId)
                foundBookInstance.StateId = request.StateId;

            if (foundBookInstance.FactCommentId != request.FactCommentId)
                foundBookInstance.FactCommentId = request.FactCommentId;

            if (!String.IsNullOrWhiteSpace(foundBookInstance.FactCommentText) && !String.IsNullOrWhiteSpace(request.FactCommentText))
            {
                if (foundBookInstance.FactCommentText.Trim().ToUpper() != request.FactCommentText.Trim().ToUpper())
                {
                    foundBookInstance.FactCommentText = request.FactCommentText;
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(request.FactCommentText))
                    foundBookInstance.FactCommentText = String.Empty;
            }

            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(foundBookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceStateResponse>(updatedBookInstance));
        }



        /// <summary>
        /// Получить данные списании экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные о списании экземпляра книги - объект BookInstanceWriteOffResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpGet("GetWriteOffInfo/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceWriteOffResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceWriteOffResponse>> GetWriteOffInfo(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<BookInstance, BookInstanceWriteOffResponse>(bookInstance));
        }


        /// <summary>
        /// Очистить данные о списании экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <returns>Возвращает данные о списании экземпляра книги - объект BookInstanceWriteOffResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("ClearWriteOffInfo/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceWriteOffResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceWriteOffResponse>> ClearWriteOffInfo(int id)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            bookInstance.WriteOffDate = null;
            bookInstance.WriteOffUserId = null;
            bookInstance.WriteOffReasonId = null;

            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(bookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceWriteOffResponse>(bookInstance));
        }


        /// <summary>
        /// Записать данные о списании экземпляра книги
        /// </summary>
        /// <param name="id">ИД экземпляра книги</param>
        /// <param name="request">Данные о списании экземпляра книги - объект типа BookInstanceUpdateWriteOffRequest</param>
        /// <returns>Возвращает данные о списании экземпляра книги - объект BookInstanceWriteOffResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Все три поля: WriteOffDate, WriteOffUserId, WriteOffReasonId должны быть заполнены</response>
        /// <response code="404">Не удалось найти экземпляр книги с указаным ИД</response>  
        [HttpPut("SetWriteOffInfo/{id:int}")]
        [ProducesResponseType(typeof(BookInstanceWriteOffResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookInstanceWriteOffResponse>> SetWriteOffInfo(int id, BookInstanceUpdateWriteOffRequest request)
        {
            var bookInstance = await _bookInstanceRepository.GetBookInstanceByIdAsync(id);

            if (bookInstance == null)
                return NotFound("Экземпляр книги с ИД = " + id.ToString() + " не найден!");

            if (request.WriteOffDate == null || request.WriteOffUserId == Guid.Empty || request.WriteOffReasonId == null || request.WriteOffReasonId <= 0)
                return BadRequest("Все три поля: WriteOffDate, WriteOffUserId, WriteOffReasonId должны быть заполнены");

            if (request.WriteOffDate > DateTime.Now)
                return BadRequest("Нельзя списывать экземпляр книги будущим временем");

            if (request.WriteOffDate != bookInstance.WriteOffDate)
                bookInstance.WriteOffDate = request.WriteOffDate;

            if (request.WriteOffUserId != bookInstance.WriteOffUserId)
                bookInstance.WriteOffUserId = request.WriteOffUserId;

            if (request.WriteOffReasonId != bookInstance.WriteOffReasonId)
                bookInstance.WriteOffReasonId = request.WriteOffReasonId;

            var updatedBookInstance = await _bookInstanceRepository.UpdateAsync(bookInstance);
            return Ok(_mapper.Map<BookInstance, BookInstanceWriteOffResponse>(bookInstance));
        }



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
