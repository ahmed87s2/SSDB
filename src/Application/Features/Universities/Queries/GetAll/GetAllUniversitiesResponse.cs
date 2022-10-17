namespace SSDB.Application.Features.Universities.Queries.GetAll
{
    public class GetAllUniversitiesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }
}