using AutoMapper;
using Catalog_DataAccess.CatalogDB;
using Catalog_Models.CatalogModels.Author;
using Catalog_Models.CatalogModels.Book;
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

            CreateMap<Publisher, PublisherItemResponse>();
            CreateMap<Publisher, PublisherItemCreateUpdateRequest>();

            CreateMap<Author, AuthorItemResponse>();
            CreateMap<Author, AuthorItemCreateUpdateRequest>();

            CreateMap<Book, BookItemResponse>();

            CreateMap<BookToAuthor, AuthorItemResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Author.LastName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Author.MiddleName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName + (String.IsNullOrWhiteSpace(src.Author.MiddleName) ? "" : src.Author.MiddleName)))
                .ForMember(dest => dest.IsForeign, opt => opt.MapFrom(src => src.Author.IsForeign))
                .ForMember(dest => dest.AddUserId, opt => opt.MapFrom(src => src.Author.AddUserId))
                .ForMember(dest => dest.AddTime, opt => opt.MapFrom(src => src.Author.AddTime))
                .ForMember(dest => dest.IsArchive, opt => opt.MapFrom(src => src.Author.IsArchive));

            CreateMap<BookToAuthor, BookToAuthorResponse>();


        }
    }
}
