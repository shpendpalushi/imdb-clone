using System.Text.Json;

namespace IMDBClone.Domain.DTO
{
    public class ExceptionDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}