namespace SSDB.Application.Requests.Catalog
{
    public class GetAllPagedPaymentsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}