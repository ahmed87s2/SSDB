using System.Linq;

namespace SSDB.Client.Infrastructure.Routes
{
    public static class StudentsEndpoints
    {
        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/Students?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
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

        public static string GetStdForReg = "api/v1/Students/GetStdForReg";

        public static string GetStudentImage(string StudentNumber)
        {
            return $"api/v1/Students/image/{StudentNumber}";
        }

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string GetById = "api/v1/Students/GetById";
        public static string GetForAddEdit = "api/v1/Students/GetByIdForAddEdit?studentNumber=";
        public static string Save = "api/v1/Students";
        public static string Delete = "api/v1/Students";
        public static string Export = "api/v1/Students/export";
    }
}