namespace Shubham_Bhawtu.Authentication.Models
{
    public class ReturnModel
    {
        public bool? Status { get; set; }
        public string? Message { get; set; }
        public int? Code { get; set; }
        public object? Data { get; set; }
    }
}
