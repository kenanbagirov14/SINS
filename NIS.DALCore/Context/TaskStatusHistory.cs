namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("TaskStatusHistory")]
  public partial class TaskStatusHistory
  {
    public int Id { get; set; }

    public int TaskStatusId { get; set; }

    public int MainTaskId { get; set; }

    public int UpdatedUserId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedDate { get; set; }

    public string Description { get; set; }

    public virtual TaskStatus TaskStatus { get; set; }
  }
}
