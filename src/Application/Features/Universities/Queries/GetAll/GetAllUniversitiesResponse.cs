namespace SSDB.Application.Features.Universities.Queries.GetAll
{
    public class GetAllUniversitiesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}