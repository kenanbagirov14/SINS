namespace NIS.BLCore.Models.RequestEmail
{
    public class RequestEmailCreateModel
  {
    public int CustomerRequestId { get; set; }
    public string UniqueId { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public string Description { get; set; }
    }
}
