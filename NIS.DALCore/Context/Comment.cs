namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Comment")]
  public partial class Comment
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Comment()
    {
      Attachment = new HashSet<Attachment>();
    }

    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Content { get; set; }

    public int MainTaskId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Attachment> Attachment { get; set; }

    public virtual MainTask MainTask { get; set; }

    public virtual User User { get; set; }
  }
}
