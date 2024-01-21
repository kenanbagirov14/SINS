using System;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.RequestEmail
{
  public class RequestEmailViewModel
  {
    public int Id { get; set; }
    public int CustomerRequestId { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
  }
}
