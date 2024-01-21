using System;

namespace NIS.BLCore.Models.RealTimeConnection
{
    public class RealTimeConnectionFindModel
  {
    public int? Id { get; set; }
    public int? UserId { get; set; }
    public string ConnectionId { get; set; }
    public string Description { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
  }
}
