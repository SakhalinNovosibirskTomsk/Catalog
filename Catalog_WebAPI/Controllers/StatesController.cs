using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels;
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

        [HttpGet("AllInArchive")]
        public async Task<ActionResult<List<StateItemResponse>>> GetAllStatesInArchiveAsync()
        {
            var gotStates = await GetStatesAsync(SD.GetAllItems.ArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateItemResponse>>(gotStates));
        }

        [HttpGet("AllNotInArchive")]
        public async Task<ActionResult<List<StateItemResponse>>> GetAllStatesNotInArchiveAsync()
        {
            var gotStates = await GetStatesAsync(SD.GetAllItems.NotArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateItemResponse>>(gotStates));
            return Ok();
        }

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


        [HttpGet("{id:int}")]
        public async Task<ActionResult<StateItemResponse>> GetSateByIdAsync(int id)
        {
            var state = await _stateRepository.GetByIdAsync(id);

            if (state == null)
                return NotFound("Статус с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<State, StateItemResponse>(state));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateStateAsync(StateItemCreateRequest request)
        {
            if (request.IsInitialState)
            {
                var initialState = _stateRepository.GetIsInitialStateAsync();
                if (initialState != null)
                    return BadRequest("Попытка добавить статус с полем IsInitialState = true. В БД уже есть статус с IsInitialState = true. Двух таких статусов быть не может.");
            }

            var stateFoundByName = _stateRepository.GetStateByNameAsync(request.Name);
            if (stateFoundByName != null)
                return BadRequest("Уже есть статус с наименованием \"" + request.Name + "\" (ИД = " + stateFoundByName.Id.ToString() + "). Двух статусов с оджинаковым наименованием быть не может.");

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
            return Created(routVar, addedState.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<StateItemResponse>> EditStateAsync(int id, StateItemUpdateRequest request)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                NotFound("Статус с Id = " + id.ToString() + " не найден.");

            var foundStateByName = await _stateRepository.GetStateByNameAsync(request.Name);

            if (foundStateByName.Id != foundState.Id)
                BadRequest("Уже есть статус с наименованием = " + request.Name + " (ИД = " + foundStateByName.Id.ToString() + ")");

            if (foundState.Name.Trim().ToUpper() != request.Name.Trim().ToUpper())
                foundState.Name = request.Name;
            if (foundState.Description.Trim().ToUpper() != request.Description.Trim().ToUpper())
                foundState.Description = request.Description;
            if (foundState.IsNeedComment != request.IsNeedComment)
                foundState.IsNeedComment = request.IsNeedComment;

            var updatedState = await _stateRepository.UpdateAsync(foundState);
            return Ok(_mapper.Map<State, StateItemResponse>(updatedState));
        }

        [HttpPut("DeleteToArchive/{id:int}")]
        public async Task<ActionResult<StateItemResponse>> DeleteStateToArchiveAsync(int id)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                NotFound("Статус с Id = " + id.ToString() + " не найден.");
            if (foundState.IsArchive == true)
                BadRequest("Статус с Id = " + id.ToString() + " уже в архиве.");
            foundState.IsArchive = true;
            var updatedState = await _stateRepository.UpdateAsync(foundState);
            return Ok(_mapper.Map<State, StateItemResponse>(updatedState));
        }

        [HttpPut("RestoreFromArchive/{id:int}")]
        public async Task<ActionResult<StateItemResponse>> RestoreStateFromArchiveAsync(int id)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                NotFound("Статус с Id = " + id.ToString() + " не найден.");
            if (foundState.IsArchive != true)
                BadRequest("Статус с Id = " + id.ToString() + " не находится в архиве. Невозможно восстановить его из архива");
            foundState.IsArchive = false;
            return Ok(await _stateRepository.UpdateAsync(foundState));
        }

        [HttpPut("SetIsInitialState/{id:int}")]
        public async Task<ActionResult<StateItemResponse>> SetIsInitialStateById(int id)
        {
            var foundState = await _stateRepository.GetByIdAsync(id);
            if (foundState == null)
                NotFound("Статус с Id = " + id.ToString() + " не найден.");
            if (foundState.IsInitialState == true)
                BadRequest("Статус с Id = " + id.ToString() + " уже является статусом по умолчанию.");

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
