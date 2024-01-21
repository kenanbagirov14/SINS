using System;

namespace NIS.BLCore.Models.Region
{
  public class RegionFindModel
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? RegionPrefix { get; set; }
    public int? Phone { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
    public int? EngineerId { get; set; }
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
  }
}
