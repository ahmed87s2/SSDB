namespace SSDB.Application.Requests.Catalog.RegistrationInfo
{
    public class GetAllPagedRegistrationInfoRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}