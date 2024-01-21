using System;

namespace NIS.BLCore.DTO
{
  public class RegionDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Phone { get; set; }
    public string Email { get; set; }
    public int? RegionPrefix { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
  }
}
