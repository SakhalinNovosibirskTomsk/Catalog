namespace Catalog_Domain.CatalogDB
{

    /// <summary>
    /// Справочник статусов состояния экземпляров книг
    /// </summary>
    public class State : BaseEntity
    {

        /// <summary>
        /// Наименование статуса состояния экземпляра книги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание статуса состояния экземпляра книги
        /// </summary>        
        public string? Description { get; set; }

        /// <summary>
        /// Признак, что состояние является исходным (например, присваивается по умолчанию при поступлении нового экземпляра книги)
        /// </summary>        
        public bool IsInitialState { get; set; } = false;

        /// <summary>
        /// Признак, что при выставлении данного состояния экземпляру книги (например, при возврате) требуется обязательный комментарий
        /// </summary>        
        public bool IsNeedComment { get; set; } = false;

        /// <summary>
        /// Признак удаления записи в архив
        /// </summary>        
        public bool IsArchive { get; set; } = false;

        public virtual ICollection<BookInstance> BookInstanceList { get; set; }

    }
}
