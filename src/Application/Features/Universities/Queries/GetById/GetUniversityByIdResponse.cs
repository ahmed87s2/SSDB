namespace SSDB.Application.Features.Universities.Queries.GetById
{
    public class GetUniversityByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}