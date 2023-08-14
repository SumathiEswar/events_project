namespace Event.Application.Models
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }
}

