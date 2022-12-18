namespace SSDB.Application.Models
{
    public class GetStudentRegistrationInfoByIdResponse
    {
        public bool Respond { get; set; }
        public int Respond_id { get; set; }
        public string Message { get; set; }
        public string Fees_id { get; set; }
        public string Student_no { get; set; }
        public string Name { get; set; }
        public string College { get; set; }
        public string Section { get; set; }
        public string Batch { get; set; }
        public string Term { get; set; }
        public int U_fees { get; set; }
        public decimal Reg_fees { get; set; }
        public int Panalty { get; set; }
        public decimal Total_amount { get; set; }
        public string Currency { get; set; }
        public string Title { get; set; }
    }


}
