namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("TaskHistory")]
  public partial class TaskHistory
  {
    public int Id { get; set; }

    public int MainTaskId { get; set; }
    public int? Type { get; set; }

    public int? UpdatedUserId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedDate { get; set; }

    public string Description { get; set; }

    public virtual MainTask MainTask { get; set; }
  }
}
