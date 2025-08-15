namespace TicketingSystemFrontend.Client.Requests
{
    public class FileUploadRequest
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string Base64Data { get; set; } = string.Empty; // store as Base64 string
    }
}
