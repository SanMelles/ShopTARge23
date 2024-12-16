namespace ShopTARge23.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // New property to display a custom error message
        public string? ErrorMessage { get; set; }
    }
}
