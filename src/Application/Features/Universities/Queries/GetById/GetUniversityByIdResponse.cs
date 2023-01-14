namespace SSDB.Application.Features.Universities.Queries.GetById
{
    public class GetUniversityByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
    }
}