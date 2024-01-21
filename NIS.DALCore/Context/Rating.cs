namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Rating")]
  public partial class Rating
  {
    [Key]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }

    public string Description { get; set; }

    public int RequestPoint { get; set; }

    public int TaskPoint { get; set; }

    //public int MainTaskId { get; set; }

    //[ForeignKey("MainTaskId")]
    //public virtual MainTask MainTask { get; set; }

  }
}
