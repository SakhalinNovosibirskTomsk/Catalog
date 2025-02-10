namespace Catalog_Models.CatalogModels
{
    public class StateItemUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsNeedComment { get; set; } = false;
    }
}
