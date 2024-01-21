using System;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.RealTimeConnection
{
  public class RealTimeConnectionViewModel
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ConnectionId { get; set; }
    public string Description { get; set; }
    //public UserDto User { get; set; }
  }
}
