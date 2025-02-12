using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels.State;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog_WebAPI.Controllers
{

    /// <summary>
    /// Статусы состояний экземпляров книг
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StatesController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public StatesController(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех статусов экземпляров книг
        /// </summary>
        /// <returns>Возвращает список всех статусов - объектов типа StateItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<StateItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<StateItemResponse>>> GetAllStatesAsync()
        {
            var gotStates = await GetStatesAsync(SD.GetAllItems.All);
            return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateItemResponse>>(gotStates));
        }

        /// <summary>
        /// Получить список всех статусов экземпляров книг находящихся в архиве
        /// </summary>
        /// <returns>Возвращает список всех статусов находящихся в архиве - объектов типа StateItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllInArchive")]
        [ProducesResponseType(typeof(List<StateItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<StateItemResponse>>> GetAllStatesInArchiveAsync()
        {
            var gotStates = await GetStatesAsync(SD.GetAllItems.ArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateItemResponse>>(gotStates));
        }

        /// <summary>
        /// Получить список всех статусов экземпляров книг находящихся НЕ в архиве
        /// </summary>
        /// <returns>Возвращает список всех статусов находящихся НЕ в архиве - объектов типа StateItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("AllNotInArchive")]
        [ProducesResponseType(typeof(List<StateItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<StateItemResponse>>> GetAllStatesNotInArchiveAsync()
        {
            var gotStates = await GetStatesAsync(SD.GetAllItems.NotArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateItemResponse>>(gotStates));
        }


        // TODO Возможно стоит вынести из контроллера в метод репозитория
        /// <summary>
        /// Вспомогательный метод возвращающий список всех статусов, статусов не в архиве или статусов в архиве в зависимости от входного параметра
        /// </summary>
        /// <param name="getAllItems">Параметр указывает возвращать все статусы, только статусы в архиве или только статусы не в архиве</param>
        /// <returns>Возвращает список статусов  - объектов типа State</returns>
        [NonAction]
        public async Task<IEnumerable<State>> GetStatesAsync(SD.GetAllItems getAllItems = SD.GetAllItems.All)
        {
            var gotStates = await _stateRepository.GetAllAsync();
            switch (getAllItems)
            {
                case SD.GetAllItems.NotArchiveOnly:
                    {
                        gotStates = gotStates.Where(u => u.IsArchive != true).ToList();
                        break;
                    }
                case SD.GetAllItems.ArchiveOnly:
                    {
                        gotStates = gotStates.Where(u => u.IsArchive == true).ToList();
                        break;
                    }
                case SD.GetAllItems.All:
                    {

                        break;
                    }
            }
            return gotStates;
        }

        /// <summary>
        /// Получить cтатус по ИД
        /// </summary>
        /// <param name="id">ИД статуса</param>
        /// <returns>Возвращает найденый по ИД статус - объект типа StateItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Статус с заданным Id не найден</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StateItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StateItemResponse>> GetStateByIdAsync(int id)
        {
            var state = await _stateRepository.GetByIdAsync(id);

            if (state == null)
                return NotFound("Статус с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<State, StateItemResponse>(state));
        }


        /// <summary>
        /// Получить cтатус по наименованию
        /// </summary>
        /// <param name="name">Наименование статуса</param>
        /// <returns>Возвращает найденый по наименование статус - объект типа StateItemResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Статус с заданным наименованием не найден</response>
        [HttpGet("GetByName/{name:alpha}")]
        [ProducesResponseType(typeof(StateItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StateItemResponse>> GetStateByNameAsync(string name)
        {
            var state = await _stateRepository.GetStateByNameAsync(name);

            if (state == null)
                return NotFound("Статус с наименованием \"" + name + "\" не найден!");

            return Ok(_mapper.Map<State, StateItemResponse>(state));
        }

        /// <summary>
        /// Создание нового статуса
        /// </summary>
        /// <param name="request">Параметры создаваемого статуса - объект типа StateItemCreateRequest</param>
        /// <returns>Возвращает созданый статус - объект типа StateItemResponse</returns>
        /// <response code="201">Успешное выполнение. Статус создан</response>
        /// <response code="400">Не удалось добавить статус. Причина описана в ответе</response>  
        [HttpPost]
        [ProducesResponseType(typeof(StateItemResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<StateItemResponse>> CreateStateAsync(StateItemCreateRequest request)
        {
            if (request.IsInitialState)
            {
                var initialState = await _stateRepository.GetIsInitialStateAsync();
                if (initialState != null)
                    return BadRequest("Попытка добавить статус с полем IsInitialState = true. В БД уже есть статус с IsInitialState = true. Двух таких статусов быть не может.");
            }

            var stateFoundByName = await _stateRepository.GetStateByNameAsync(request.Name);
            if (stateFoundByName != null)
                return BadRequest("Уже есть статус с наименованием \"" + request.Name + "\" (ИД = " + stateFoundByName.Id.ToString() + "). Двух статусов с одинаковым наименованием быть не может.");

            var addedState = await _stateRepository.AddAsync(
                new State
                {
                    Name = request.Name,
                    Description = request.Description,
                    IsNeedComment = request.IsNeedComment,
                    IsInitialState = request.IsInitialState,
                    IsArchive = request.IsArchive,
                });

            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedState.Id.ToString();
            }
            return Created(routVar, _mapper.Map<State, StateItemResponse>(addedState));
        }

        /// <summary>
        /// Изменение существующего статуса
        /// </summary>
        /// <param name="id">ИД изменяемого статуса</param>
        /// <param name="request">Новые данные изменяемого статуса - объект типа StateItemUpdateRequest</param>
        /// <returns>Возвращает данные изменённого статуса - объект StateItemResponse</returns>
        /// <response code="200">Успешное выполнение. Статус изменён</response>
        /// <response code="400">Не удалось изменить статус. Причина описана в ответе</response>  
        /// <response code="404">Не удалось найти статус с указаным ИД</response>  
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(StateItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StateItemResponse>> EditStateAsync(int id, StateItemUpdateRequest request)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                return NotFound("Статус с Id = " + id.ToString() + " не найден.");

            var foundStateByName = await _stateRepository.GetStateByNameAsync(request.Name);

            if (foundStateByName != null)
                if (foundStateByName.Id != foundState.Id)
                    return BadRequest("Уже есть статус с наименованием = " + request.Name + " (ИД = " + foundStateByName.Id.ToString() + ")");

            // TODO Сделать и протестировать регистронезависимое сравнение строк
            if (foundState.Name != request.Name)
                foundState.Name = request.Name;

            // TODO Сделать и протестировать регистронезависимое сравнение строк
            if (foundState.Description != request.Description)
                foundState.Description = request.Description;
            if (foundState.IsNeedComment != request.IsNeedComment)
                foundState.IsNeedComment = request.IsNeedComment;

            var updatedState = await _stateRepository.UpdateAsync(foundState);
            return Ok(_mapper.Map<State, StateItemResponse>(updatedState));
        }



        /// <summary>
        /// Удалить статус с указаным ИД в архив
        /// </summary>
        /// <param name="id">ИД удаляемого в архив статуса</param>
        /// <returns>Возвращает данные удалённого в архив статуса - объект StateItemResponse</returns>
        /// <response code="200">Успешное выполнение. Статус удалён в архив</response>
        /// <response code="400">Не удалось удалить статус в архив, так как статус уже в архиве</response>  
        /// <response code="404">Не удалось найти статус с указаным ИД</response>  
        [HttpPut("DeleteToArchive/{id:int}")]
        [ProducesResponseType(typeof(StateItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StateItemResponse>> DeleteStateToArchiveAsync(int id)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                return NotFound("Статус с Id = " + id.ToString() + " не найден.");
            if (foundState.IsArchive == true)
                return BadRequest("Статус с Id = " + id.ToString() + " уже в архиве.");
            if (foundState.IsInitialState == true)
                return BadRequest("Статус с Id = " + id.ToString() + " является статусом по умолчанию для новых экземпляров (IsInitialState = true). Чтобы удалить его в архив нужно выбрать другой статус к качестве статуса по умолчанию.");
            foundState.IsArchive = true;
            var updatedState = await _stateRepository.UpdateAsync(foundState);
            return Ok(_mapper.Map<State, StateItemResponse>(updatedState));
        }


        /// <summary>
        /// Восстановить статус с указаным ИД из архива
        /// </summary>
        /// <param name="id">ИД восстанавливаемого из архива статуса</param>
        /// <returns>Возвращает данные восстановленного из архива статуса - объект StateItemResponse</returns>
        /// <response code="200">Успешное выполнение. Статус восстановлен из архива архив</response>
        /// <response code="400">Не удалось восстановить статус из архива, так как статус уже находится не в архиве</response>  
        /// <response code="404">Не удалось найти статус с указаным ИД</response>  
        [HttpPut("RestoreFromArchive/{id:int}")]
        [ProducesResponseType(typeof(StateItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StateItemResponse>> RestoreStateFromArchiveAsync(int id)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                return NotFound("Статус с Id = " + id.ToString() + " не найден.");
            if (foundState.IsArchive != true)
                return BadRequest("Статус с Id = " + id.ToString() + " не находится в архиве. Невозможно восстановить его из архива");
            foundState.IsArchive = false;
            return Ok(await _stateRepository.UpdateAsync(foundState));
        }


        /// <summary>
        /// Утановить статус как статус по умолчанию для новых экземпляров книг
        /// </summary>
        /// <param name="id">ИД статуса</param>
        /// <returns>Возвращает данные статуса, установленного в качестве статуса по умолчанию для новых экземпляров книг - объект StateItemResponse</returns>
        /// <response code="200">Успешное выполнение. Статус установлен в качестве статуса по умолчанию для новых экземпляров книг</response>
        /// <response code="400">Не удалось выполнить операцию, так как статус уже является статусом по умолчанию для новых экземпляров книг</response>  
        /// <response code="404">Не удалось найти статус с указаным ИД</response>  
        [HttpPut("SetIsInitialState/{id:int}")]
        [ProducesResponseType(typeof(StateItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StateItemResponse>> SetIsInitialStateById(int id)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                return NotFound("Статус с Id = " + id.ToString() + " не найден.");
            if (foundState.IsInitialState)
                return BadRequest("Статус с Id = " + id.ToString() + " уже является статусом по умолчанию.");

            var currentInitialState = await _stateRepository.GetIsInitialStateAsync();

            if (currentInitialState != null)
            {
                currentInitialState.IsInitialState = false;
                await _stateRepository.UpdateAsync(foundState);
            }
            foundState.IsInitialState = true;
            var updatedState = await _stateRepository.UpdateAsync(foundState);
            return Ok(_mapper.Map<State, StateItemResponse>(updatedState));
        }
    }
}
