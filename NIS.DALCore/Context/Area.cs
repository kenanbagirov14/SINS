namespace NIS.DALCore.Context
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;

  //[Table("Area")]
  public partial class Area
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Area()
    {
      Node = new HashSet<Node>();
    }

    public int Id { get; set; }

    public int RegionId { get; set; }

    public string Name { get; set; }

    public int? AreaPrefix { get; set; }
    //[System.ComponentModel.DataAnnotations.Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }

    public string Description { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Node> Node { get; set; }

    public virtual Region Region { get; set; }
  }
}
