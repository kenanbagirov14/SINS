namespace NIS.DALCore.Context
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("UserSettings")]
  public partial class UserSettings
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int Type { get; set; }

    public int UserId { get; set; }

    [Required]
    public string Settings { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? CreatedDate { get; set; }

    public string Description { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

  }
}
