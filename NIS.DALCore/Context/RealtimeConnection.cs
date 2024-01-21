namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("RealTimeConnection")]
  public partial class RealTimeConnection
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ConnectionId { get; set; }
    public string Description { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
  }
}
