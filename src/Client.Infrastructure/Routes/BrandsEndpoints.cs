namespace SSDB.Client.Infrastructure.Routes
{
    public static class UniversitiesEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/Universities/export";

        public static string GetAll = "api/v1/Universities";
        public static string Delete = "api/v1/Universities";
        public static string Save = "api/v1/Universities";
        public static string GetCount = "api/v1/Universities/count";
    }
}