using AutoMapper;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels;

namespace Catalog_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<State, StateItemResponse>().ReverseMap();
        }
    }
}
