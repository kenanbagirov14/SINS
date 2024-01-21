namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InjuryType")]
    public partial class InjuryType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InjuryType()
        {
            MainTask = new HashSet<MainTask>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? DepartmentId { get; set; }

        public string Description { get; set; }

        public int? ExecutionDay { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }
        [InverseProperty("InjuryType")]
        public virtual ICollection<MainTask> MainTask { get; set; }

        [InverseProperty("RealInjuryType")]
        public virtual ICollection<MainTask> RealMainTask { get; set; }
        public virtual Department Department { get; set; }
    }
}
