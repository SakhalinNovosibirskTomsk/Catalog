using AutoMapper;
using Catalog_Business.Repository.IRepository;
using Catalog_Common;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("All")]
        public async Task<ActionResult<List<StateItemResponse>>> GetAllStatesAsync()
        {
            var gotStates = await GetStatesAsync(SD.GetAllItems.All);
            return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateItemResponse>>(gotStates));
            return Ok();
        }

        [HttpGet("AllInArchive")]
        public async Task<ActionResult<List<StateItemResponse>>> GetAllStatesInArchiveAsync()
        {
            var gotStates = await GetStatesAsync(SD.GetAllItems.ArchiveOnly);
            return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateItemResponse>>(gotStates));
            return Ok();
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
        public async Task<ActionResult<State>> GetSateByIdAsync(int id)
        {
            var state = await _stateRepository.GetByIdAsync(id);

            if (state == null)
                return NotFound("Статус с ID = " + id.ToString() + " не найден!");

            return Ok(_mapper.Map<State, StateItemResponse>(state));
        }

        [HttpPost]
        public async Task<ActionResult<State>> CreateStateAsync(StateItemRequest request)
        {

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditStateAsync(int id, StateItemRequest request)
        {
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStateAsync(Guid id)
        {
            return NoContent();
        }
    }
}
