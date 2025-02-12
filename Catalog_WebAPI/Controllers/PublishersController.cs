using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
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
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;
        public PublishersController(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех издателей
        /// </summary>
        /// <returns>Возвращает список всех издателей - объектов типа PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<PublisherItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<PublisherItemResponse>>> GetAllPublishersAsync()
        {
            var gotPublishers = await GetPublishersAsync(SD.GetAllItems.All);
            return Ok(_mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherItemResponse>>(gotPublishers));
        }

        /// <summary>
        /// Получить список всех издателей находящихся в архиве
        /// </summary>
        /// <returns>Возвращает список всех издателей находящихся в архиве - объектов типа PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllInArchive")]
        [ProducesResponseType(typeof(List<PublisherItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<PublisherItemResponse>>> GetAllPublishersInArchiveAsync()
        {
            var gotPublishers = await GetPublishersAsync(SD.GetAllItems.ArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherItemResponse>>(gotPublishers));
        }

        /// <summary>
        /// Получить список всех издателей находящихся НЕ в архиве
        /// </summary>
        /// <returns>Возвращает список всех издателей находящихся НЕ в архиве - объектов типа PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllNotInArchive")]
        [ProducesResponseType(typeof(List<PublisherItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<PublisherItemResponse>>> GetAllPublishersNotInArchiveAsync()
        {
            var gotPublishers = await GetPublishersAsync(SD.GetAllItems.NotArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherItemResponse>>(gotPublishers));
        }


        // TODO Возможно стоит вынести из контроллера в метод репозитория
        /// <summary>
        /// Вспомогательный метод возвращающий список всех издателей, издателей не в архиве или издателей в архиве в зависимости от входного параметра
        /// </summary>
        /// <param name="getAllItems">Параметр указывает возвращать всех издателей, только издателей в архиве или только издателей не в архиве</param>
        /// <returns>Возвращает список издателей  - объектов типа Publisher</returns>
        [NonAction]
        public async Task<IEnumerable<Publisher>> GetPublishersAsync(SD.GetAllItems getAllItems = SD.GetAllItems.All)
        {
            var gotPublishers = await _publisherRepository.GetAllAsync();
            switch (getAllItems)
            {
                case SD.GetAllItems.NotArchiveOnly:
                    {
                        gotPublishers = gotPublishers.Where(u => u.IsArchive != true).ToList();
                        break;
                    }
                case SD.GetAllItems.ArchiveOnly:
                    {
                        gotPublishers = gotPublishers.Where(u => u.IsArchive == true).ToList();
                        break;
                    }
                case SD.GetAllItems.All:
                    {
                        break;
                    }
            }
            return gotPublishers;
        }

        /// <summary>
        /// Получить издателя по ИД
        /// </summary>
        /// <param name="id">ИД издателя</param>
        /// <returns>Возвращает найденого по ИД издателя - объект типа PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Издатель с заданным Id не найден</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PublisherItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PublisherItemResponse>> GetPublisherByIdAsync(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);

            if (publisher == null)
                return NotFound("Издатель с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<Publisher, PublisherItemResponse>(publisher));
        }

        /// <summary>
        /// Получить издателя по наименованию
        /// </summary>
        /// <param name="name">Наименование издателя</param>
        /// <returns>Возвращает найденого по наименованию издателя - объект типа PublisherItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Издатель с заданным наименованием не найден</response>
        [HttpGet("GetByName/{name:alpha}")]
        [ProducesResponseType(typeof(PublisherItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PublisherItemResponse>> GetPublisherByNameAsync(string name)
        {
            var publisher = await _publisherRepository.GetPublisherByNameAsync(name);

            if (publisher == null)
                return NotFound("Издатель с наименованием \"" + name + "\" не найден!");

            return Ok(_mapper.Map<Publisher, PublisherItemResponse>(publisher));
        }


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

            // TODO Сделать и протестировать регистронезависимое сравнение строк
            if (foundPublisher.Name != request.Name)
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
            return Ok(await _publisherRepository.UpdateAsync(foundPublisher));
        }
    }
}
