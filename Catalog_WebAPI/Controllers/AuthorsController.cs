using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels.Author;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog_WebAPI.Controllers
{

    // TODO посмотреть возможность выделение в отдельный класс или интрефейс общих частей контроллеров AuthorsController, PublisherController, StatesController

    /// <summary>
    /// Авторы
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех авторов
        /// </summary>
        /// <returns>Возвращает список всех авторов - объектов типа AuthorItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<AuthorItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<AuthorItemResponse>>> GetAllAuthorsAsync()
        {
            var gotAuthors = await GetAuthorsAsync(SD.GetAllItems.All);
            return Ok(_mapper.Map<IEnumerable<Author>, IEnumerable<AuthorItemResponse>>(gotAuthors));
        }

        /// <summary>
        /// Получить список всех авторов находящихся в архиве
        /// </summary>
        /// <returns>Возвращает список всех авторов находящихся в архиве - объектов типа AuthorItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllInArchive")]
        [ProducesResponseType(typeof(List<AuthorItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<AuthorItemResponse>>> GetAllAuthorsInArchiveAsync()
        {
            var gotAuthors = await GetAuthorsAsync(SD.GetAllItems.ArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<Author>, IEnumerable<AuthorItemResponse>>(gotAuthors));
        }

        /// <summary>
        /// Получить список всех авторов находящихся НЕ в архиве
        /// </summary>
        /// <returns>Возвращает список всех авторов находящихся НЕ в архиве - объектов типа AuthorItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllNotInArchive")]
        [ProducesResponseType(typeof(List<AuthorItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<AuthorItemResponse>>> GetAllAuthorsNotInArchiveAsync()
        {
            var gotAuthors = await GetAuthorsAsync(SD.GetAllItems.NotArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<Author>, IEnumerable<AuthorItemResponse>>(gotAuthors));
        }


        // TODO Возможно стоит вынести из контроллера в метод репозитория
        /// <summary>
        /// Вспомогательный метод возвращающий список всех авторов, авторов не в архиве или авторов в архиве в зависимости от входного параметра
        /// </summary>
        /// <param name="getAllItems">Параметр указывает возвращать всех авторов, только авторов в архиве или только авторов не в архиве</param>
        /// <returns>Возвращает список авторов  - объектов типа Author</returns>
        [NonAction]
        public async Task<IEnumerable<Author>> GetAuthorsAsync(SD.GetAllItems getAllItems = SD.GetAllItems.All)
        {
            var gotAuthors = await _authorRepository.GetAllAsync();
            switch (getAllItems)
            {
                case SD.GetAllItems.NotArchiveOnly:
                    {
                        gotAuthors = gotAuthors.Where(u => u.IsArchive != true).ToList();
                        break;
                    }
                case SD.GetAllItems.ArchiveOnly:
                    {
                        gotAuthors = gotAuthors.Where(u => u.IsArchive == true).ToList();
                        break;
                    }
                case SD.GetAllItems.All:
                    {
                        break;
                    }
            }
            return gotAuthors;
        }

        /// <summary>
        /// Получить автора по ИД
        /// </summary>
        /// <param name="id">ИД автора</param>
        /// <returns>Возвращает найденого по ИД автора - объект типа AuthorItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Автор с заданным Id не найден</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AuthorItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AuthorItemResponse>> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return NotFound("Автор с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<Author, AuthorItemResponse>(author));
        }


        /// <summary>
        /// Получить автора по имени, фамилии, отчеству
        /// </summary>
        /// <param name="firstname"> Имя автора </param>
        /// <param name="lastname"> Фамилия автора </param>
        /// <param name="middlename"> Отчество автора </param>
        /// <returns>Возвращает найденого автора - объект типа AuthorItemResponse</returns>
        [HttpGet("GetByName/{firstname:alpha}/{lastname:alpha}/{middlename:alpha}")]
        [ProducesResponseType(typeof(AuthorItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AuthorItemResponse>> GetAuthorByFullNameAsync(string firstname, string lastname, string? middlename)
        {
            var author = await _authorRepository.GetAuthorByFullNameAsync(firstname, lastname, middlename);

            if (author == null)
                return NotFound("Автор \"" + firstname + " " + lastname + " " + middlename + "\" не найден!");

            return Ok(_mapper.Map<Author, AuthorItemResponse>(author));
        }


        /// <summary>
        /// Создание нового автора
        /// </summary>
        /// <param name="request">Параметры создаваемого автора - объект типа AuthorItemCreateUpdateRequest</param>
        /// <returns>Возвращает созданого автора - объект типа AuthorItemResponse</returns>
        /// <response code="201">Успешное выполнение. Автор создан</response>
        /// <response code="400">Не удалось добавить автора. Причина описана в ответе</response>  
        [HttpPost]
        [ProducesResponseType(typeof(AuthorItemResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AuthorItemResponse>> CreateAuthorAsync(AuthorItemCreateUpdateRequest request)
        {
            var authorFoundByFullName = await _authorRepository.GetAuthorByFullNameAsync(request.FirstName, request.LastName, request.MiddleName);
            if (authorFoundByFullName != null)
                return BadRequest("Уже есть автор \"" + request.FirstName + " " + request.LastName + " " + request.MiddleName + "\" (ИД = " + authorFoundByFullName.Id.ToString() + ").");

            var addedAuthor = await _authorRepository.AddAsync(
                new Author
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    IsForeign = request.IsForeign,
                    // TODO Пользователя добавившего запись в будущем нужно брать реального
                    AddUserId = SD.UserIdForInitialData,
                    AddTime = DateTime.Now,
                    IsArchive = false,
                });

            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedAuthor.Id.ToString();
            }
            return Created(routVar, _mapper.Map<Author, AuthorItemResponse>(addedAuthor));
        }

        /// <summary>
        /// Изменение существующего автора
        /// </summary>
        /// <param name="id">ИД изменяемого автора</param>
        /// <param name="request">Новые данные изменяемого автора - объект типа AuthorItemCreateUpdateRequest</param>
        /// <returns>Возвращает данные изменённого автора - объект AuthorItemResponse</returns>
        /// <response code="200">Успешное выполнение. Автор изменён</response>
        /// <response code="400">Не удалось изменить автора. Причина описана в ответе</response>  
        /// <response code="404">Не удалось найти автора с указаным ИД</response>  
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(AuthorItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AuthorItemResponse>> EditAuthorAsync(int id, AuthorItemCreateUpdateRequest request)
        {
            var foundAuthor = await _authorRepository.GetByIdAsync(id);
            if (foundAuthor == null)
                return NotFound("Автор с Id = " + id.ToString() + " не найден.");

            var foundAuthorByFullName = await _authorRepository.GetAuthorByFullNameAsync(request.FirstName, request.LastName, request.MiddleName);

            if (foundAuthorByFullName != null)
                if (foundAuthorByFullName.Id != foundAuthor.Id)
                    return BadRequest("Уже есть автор \"" + request.FirstName + " " + request.LastName + " " + request.MiddleName + "\" (ИД = " + foundAuthorByFullName.Id.ToString() + ")");

            // TODO Сделать и протестировать регистронезависимое сравнение строк
            if (foundAuthor.FirstName != request.FirstName)
                foundAuthor.FirstName = request.FirstName;

            // TODO Сделать и протестировать регистронезависимое сравнение строк
            if (foundAuthor.LastName != request.LastName)
                foundAuthor.LastName = request.LastName;

            // TODO Сделать и протестировать регистронезависимое сравнение строк
            if (foundAuthor.MiddleName != request.MiddleName)
                foundAuthor.MiddleName = request.MiddleName;

            if (foundAuthor.IsForeign != request.IsForeign)
                foundAuthor.IsForeign = request.IsForeign;

            var updatedAuthor = await _authorRepository.UpdateAsync(foundAuthor);
            return Ok(_mapper.Map<Author, AuthorItemResponse>(updatedAuthor));
        }



        /// <summary>
        /// Удалить автора с указаным ИД в архив
        /// </summary>
        /// <param name="id">ИД удаляемого в архив автора</param>
        /// <returns>Возвращает данные удалённого в архив автора - объект AuthorItemResponse</returns>
        /// <response code="200">Успешное выполнение. Автор удалён в архив</response>
        /// <response code="400">Не удалось удалить автора в архив, так как автор уже в архиве</response>  
        /// <response code="404">Не удалось найти автора с указаным ИД</response>  
        [HttpPut("DeleteToArchive/{id:int}")]
        [ProducesResponseType(typeof(AuthorItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AuthorItemResponse>> DeleteAuthorToArchiveAsync(int id)
        {
            var foundAuthor = await _authorRepository.GetByIdAsync(id);
            if (foundAuthor == null)
                return NotFound("Автор с Id = " + id.ToString() + " не найден.");
            if (foundAuthor.IsArchive == true)
                return BadRequest("Автор с Id = " + id.ToString() + " уже в архиве.");
            foundAuthor.IsArchive = true;
            var updatedAuthor = await _authorRepository.UpdateAsync(foundAuthor);
            return Ok(_mapper.Map<Author, AuthorItemResponse>(updatedAuthor));
        }


        /// <summary>
        /// Восстановить автора с указаным ИД из архива
        /// </summary>
        /// <param name="id">ИД восстанавливаемого из архива автора</param>
        /// <returns>Возвращает данные восстановленного из архива автора - объект AuthorItemResponse</returns>
        /// <response code="200">Успешное выполнение. Автор восстановлен из архива</response>
        /// <response code="400">Не удалось восстановить автора из архива, так как автор уже не в архиве</response>  
        /// <response code="404">Не удалось найти автора с указаным ИД</response>  
        [HttpPut("RestoreFromArchive/{id:int}")]
        [ProducesResponseType(typeof(AuthorItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AuthorItemResponse>> RestoreAuthorFromArchiveAsync(int id)
        {
            var foundAuthor = await _authorRepository.GetByIdAsync(id);
            if (foundAuthor == null)
                return NotFound("Автор с Id = " + id.ToString() + " не найден.");
            if (foundAuthor.IsArchive != true)
                return BadRequest("Автор с Id = " + id.ToString() + " не находится в архиве. Невозможно восстановить его из архива");
            foundAuthor.IsArchive = false;
            return Ok(await _authorRepository.UpdateAsync(foundAuthor));
        }
    }
}
