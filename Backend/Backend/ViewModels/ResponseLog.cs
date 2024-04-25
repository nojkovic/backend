namespace Backend.ViewModels
{
    public class ResponseLog
    {
        public string Message { get; set; }
        public string Status { get; set; } = "error";
        public string JwtToken { get; set; }

    }
}
