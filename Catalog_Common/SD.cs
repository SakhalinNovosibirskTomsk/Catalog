namespace Catalog_Common
{
    public static class SD
    {

        public static int SqlCommandConnectionTimeout = 180;

        public static string BookECopyPath = "c:\\temp";

        public static Guid UserIdForInitialData = new Guid("476ea54a-18b9-45e8-8dc2-1dccac0dd1d6");

        public enum GetAllItems
        {
            ArchiveOnly,
            NotArchiveOnly,
            All
        }


        public enum BookInstancesFags
        {
            IsCheckedOut,
            IsBooked,
            IsWroteOff,
            /// <summary>
            /// все 3 флага не выставлены. Экземпляр можно выдавать, бронировать, списывать
            /// </summary>
            IsFree,
            /// <summary>
            /// один или более 3-х флагов выставлены. Экземпляр или выдан читателю, или забронирован, или списан
            /// </summary>
            IsBusy
        }
    }
}
