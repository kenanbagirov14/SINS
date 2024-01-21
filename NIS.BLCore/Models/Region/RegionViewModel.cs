using System;
using System.Collections.Generic;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Region
{
  public class RegionViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }    public int? Phone { get; set; }
    public int? RegionPrefix { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }
    public int? EngineerId { get; set; }
    public int? TotalCount { get; set; }

    public List<AreaDto> Area { get; set; }
    //public List<RequestDto> CustomerRequest { get; set; }
    //public List<RequestTypeDto> CustomerRequestType { get; set; }
    //public EngineerDto Engineer { get; set; }
    //public List<TaskDto> MainTask { get; set; }
  }
}
