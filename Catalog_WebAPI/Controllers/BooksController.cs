using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels.Book;
using Catalog_Models.CatalogModels.Publisher;
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
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksController(IPublisherRepository publisherRepository, IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
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
            return Ok(_mapper.Map<IEnumerable<Book>, IEnumerable<BookItemResponse>>(gotBooks));
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
        public async Task<ActionResult<List<BookItemResponse>>> GetAllPublishersNotInArchiveAsync()
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

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Stopped


        /// <summary>
        /// Создание нового издателя
        /// </summary>
        /// <param name="request">Параметры создаваемого издателя - объект типа PublisherItemCreateUpdateRequest</param>
        /// <returns>Возвращает созданого издателя - объект типа PublisherItemResponse</returns>
        /// <response code="201">Успешное выполнение. Издатель создан</response>
        /// <response code="400">Не удалось добавить издателя. Причина описана в ответе</response>  
        [HttpPost]
        [ProducesResponseType(typeof(PublisherItemResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PublisherItemResponse>> CreatePublisherAsync(PublisherItemCreateUpdateRequest request)
        {
            var publisherFoundByName = await _publisherRepository.GetPublisherByNameAsync(request.Name);
            if (publisherFoundByName != null)
                return BadRequest("Уже есть издатель с наименованием \"" + request.Name + "\" (ИД = " + publisherFoundByName.Id.ToString() + "). Двух издателей с одинаковым наименованием быть не может.");

            var addedPublisher = await _publisherRepository.AddAsync(
                new Publisher
                {
                    Name = request.Name,
                    // TODO Пользователя добавившего запись в будущем нужно брать реального
                    AddUserId = SD.UserIdForInitialData,
                    AddTime = DateTime.Now,
                    IsArchive = false,
                });

            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedPublisher.Id.ToString();
            }
            return Created(routVar, _mapper.Map<Publisher, PublisherItemResponse>(addedPublisher));
        }

        /// <summary>
        /// Изменение существующего издателя
        /// </summary>
        /// <param name="id">ИД изменяемого издателя</param>
        /// <param name="request">Новые данные изменяемого издателя - объект типа PublisherItemCreateUpdateRequest</param>
        /// <returns>Возвращает данные изменённого издателдя - объект PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение. Издатель изменён</response>
        /// <response code="400">Не удалось изменить издателя. Причина описана в ответе</response>  
        /// <response code="404">Не удалось найти издателя с указаным ИД</response>  
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PublisherItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PublisherItemResponse>> EditPublisherAsync(int id, PublisherItemCreateUpdateRequest request)
        {
            var foundPublisher = await _publisherRepository.GetByIdAsync(id);
            if (foundPublisher == null)
                return NotFound("Издатель с Id = " + id.ToString() + " не найден.");

            var foundPublisherByName = await _publisherRepository.GetPublisherByNameAsync(request.Name);

            if (foundPublisherByName != null)
                if (foundPublisherByName.Id != foundPublisher.Id)
                    return BadRequest("Уже есть издатель с наименованием = " + request.Name + " (ИД = " + foundPublisherByName.Id.ToString() + ")");

            if (foundPublisher.Name.Trim().ToUpper() != request.Name.Trim().ToUpper())
                foundPublisher.Name = request.Name;

            var updatedPublisher = await _publisherRepository.UpdateAsync(foundPublisher);
            return Ok(_mapper.Map<Publisher, PublisherItemResponse>(updatedPublisher));
        }



        /// <summary>
        /// Удалить издателя с указаным ИД в архив
        /// </summary>
        /// <param name="id">ИД удаляемого в архив издателя</param>
        /// <returns>Возвращает данные удалённого в архив издателя - объект PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение. Издатель удалён в архив</response>
        /// <response code="400">Не удалось удалить издателя в архив, так как издатель уже в архиве</response>  
        /// <response code="404">Не удалось найти издателя с указаным ИД</response>  
        [HttpPut("DeleteToArchive/{id:int}")]
        [ProducesResponseType(typeof(PublisherItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PublisherItemResponse>> DeletePublisherToArchiveAsync(int id)
        {
            var foundPublisher = await _publisherRepository.GetByIdAsync(id);
            if (foundPublisher == null)
                return NotFound("Издатель с Id = " + id.ToString() + " не найден.");
            if (foundPublisher.IsArchive == true)
                return BadRequest("Издатель с Id = " + id.ToString() + " уже в архиве.");
            foundPublisher.IsArchive = true;
            var updatedPublisher = await _publisherRepository.UpdateAsync(foundPublisher);
            return Ok(_mapper.Map<Publisher, PublisherItemResponse>(updatedPublisher));
        }


        /// <summary>
        /// Восстановить издателя с указаным ИД из архива
        /// </summary>
        /// <param name="id">ИД восстанавливаемого из архива издателя</param>
        /// <returns>Возвращает данные восстановленного из архива издателя - объект PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение. Издатель восстановлен из архива</response>
        /// <response code="400">Не удалось восстановить издателя из архива, так как издатель уже не в архиве</response>  
        /// <response code="404">Не удалось найти издателя с указаным ИД</response>  
        [HttpPut("RestoreFromArchive/{id:int}")]
        [ProducesResponseType(typeof(PublisherItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PublisherItemResponse>> RestorePublisherFromArchiveAsync(int id)
        {
            var foundPublisher = await _publisherRepository.GetByIdAsync(id);
            if (foundPublisher == null)
                return NotFound("Издатель с Id = " + id.ToString() + " не найден.");
            if (foundPublisher.IsArchive != true)
                return BadRequest("Издатель с Id = " + id.ToString() + " не находится в архиве. Невозможно восстановить его из архива");
            foundPublisher.IsArchive = false;
            var updatedPublisher = await _publisherRepository.UpdateAsync(foundPublisher);
            return Ok(_mapper.Map<Publisher, PublisherItemResponse>(updatedPublisher));
        }
    }
}
