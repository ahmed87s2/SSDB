namespace SSDB.Application.Requests.Catalog
{
    public class GetAllPagedRegistrationsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}