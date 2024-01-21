namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Attachment")]
  public partial class Attachment
  {
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public int Id { get; set; }

    public string FilePath { get; set; }

    public string Description { get; set; }

    public int? FileType { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }

    public int? MainTaskId { get; set; }

    public int? CustomerRequestId { get; set; }

    public int? CommentId { get; set; }

    public virtual Comment Comment { get; set; }

    public virtual CustomerRequest CustomerRequest { get; set; }

    public virtual MainTask MainTask { get; set; }
  }
}
