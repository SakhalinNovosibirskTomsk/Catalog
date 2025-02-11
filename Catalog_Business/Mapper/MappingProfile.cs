using AutoMapper;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels.Publisher;
using Catalog_Models.CatalogModels.State;

namespace Catalog_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<State, StateItemResponse>();
            CreateMap<State, StateItemCreateRequest>();
            CreateMap<State, PublisherItemResponse>();
            CreateMap<State, PublisherItemCreateUpdateRequest>();
        }
    }
}
