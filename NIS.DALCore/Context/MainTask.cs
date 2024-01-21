namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MainTask")]
    public partial class MainTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MainTask()
        {
            Attachment = new HashSet<Attachment>();
            Comment = new HashSet<Comment>();
            Tag = new HashSet<Tag>();
            ChildTasks = new HashSet<MainTask>();
        }

        [Key]
        public int Id { get; set; }

        public int GeneratedUserId { get; set; }

        public int? CustomerRequestId { get; set; }

        public int? ExecutorUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public int? DepartmentId { get; set; }

        public int? TaskStatusId { get; set; }

        public int? ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int? MainTaskId { get; set; }

        public string Note { get; set; }

        public int? RatingId { get; set; }
        public int? UpdatedUserId { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }

        public virtual CustomerRequest CustomerRequest { get; set; }

        public virtual Department Department { get; set; }


        public int? InjuryTypeId { get; set; }
        [ForeignKey("InjuryTypeId")]
        public virtual InjuryType InjuryType { get; set; }

        public int? RealInjuryTypeId { get; set; }
        [ForeignKey("RealInjuryTypeId")]
        public virtual InjuryType RealInjuryType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainTask> ChildTasks { get; set; }
        public virtual ICollection<TaskHistory> TaskHistory { get; set; }

        public virtual MainTask ParentTask { get; set; }

        public virtual Project Project { get; set; }

        public virtual TaskStatus TaskStatus { get; set; }

        public virtual User ExecutorUser { get; set; }

        [ForeignKey("UpdatedUserId")]
        public virtual User UpdateddUser { get; set; }
        public virtual User GeneratedUser { get; set; }

        public virtual Rating Rating { get; set; }
    }
}
