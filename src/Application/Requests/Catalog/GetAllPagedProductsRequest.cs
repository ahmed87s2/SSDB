namespace SSDB.Application.Requests.Catalog
{
    public class GetAllPagedStudentsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}