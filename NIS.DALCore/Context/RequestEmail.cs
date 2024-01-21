namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("RequestEmail")]
  public partial class RequestEmail
  {
    [Key]
    public int Id { get; set; }
    public int CustomerRequestId { get; set; }
    public string Subject { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }
    public string Description { get; set; }


    [ForeignKey("CustomerRequestId")]
    public virtual CustomerRequest CustomerRequest { get; set; }
  }
}
