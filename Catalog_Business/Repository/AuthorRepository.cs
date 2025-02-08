﻿using Catalog_Business.Repository.IRepository;
using Catalog_DataAccess;
using Catalog_DataAccess.CatalogDB;

namespace Catalog_Business.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext _db) : base(_db)
        {
        }
    }
}
