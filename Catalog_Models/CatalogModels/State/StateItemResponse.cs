namespace Catalog_Models.CatalogModels.State
{
    public class StateItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsInitialState { get; set; } = false;
        public bool IsNeedComment { get; set; } = false;
        public bool IsArchive { get; set; }
    }
}
