namespace SSDB.Application.Features.Students.Queries.GetAllPaged
{
    public class GetAllPagedStudentsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Barcode { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public string University { get; set; }
        public int UniversityId { get; set; }
    }
}