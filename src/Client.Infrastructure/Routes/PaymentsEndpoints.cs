using System.Linq;

namespace SSDB.Client.Infrastructure.Routes
{
    public static class PaymentsEndpoints
    {
        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/Payments?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;
        }

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string GetById = "api/v1/Payments/GetById";
        public static string GetForAddEdit = "api/v1/Payments/GetByIdForAddEdit?id=";
        public static string Save = "api/v1/Payments";
        public static string Export = "api/v1/Payments/export";
        public static string UpdatePaymentInfo = "api/v1/Payments/UpdatePaymentInfo";
    }
}