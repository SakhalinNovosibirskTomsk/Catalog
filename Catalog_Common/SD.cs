namespace Catalog_Common
{
    public static class SD
    {

        public enum DbConnectionMode
        {
            SqlLight,
            PostgreSQL,
            MSSQL
        }

        public static int SqlCommandConnectionTimeout = 180;
    }
}
