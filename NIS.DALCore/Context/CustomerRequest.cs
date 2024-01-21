namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CustomerRequest")]
    public partial class CustomerRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerRequest()
        {
            Attachment = new HashSet<Attachment>();
            MainTask = new HashSet<MainTask>();
            RequestStatusHistory = new HashSet<RequestStatusHistory>();
        }

        public int Id { get; set; }

        public int CustomerRequestTypeId { get; set; }

        public int? DepartmentId { get; set; }

        public int CreatedUserId { get; set; }

        public string CustomerName { get; set; }
        public string Email { get; set; }

        public int? CustomerNumber { get; set; }

        [StringLength(50)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string ContractNumber { get; set; }

        [StringLength(50)]
        public string AON { get; set; }

        public string Text { get; set; }

        public DateTime? StartDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }

        public string Description { get; set; }



        public int? SourceTypeId { get; set; }

        public int? RegionId { get; set; }

        public int? RequestStatusId { get; set; }

        public int? RatingId { get; set; }
        public string MailUniqueId { get; set; }


        
        public virtual ICollection<Attachment> Attachment { get; set; }

        public virtual Region Region { get; set; }

        public virtual RequestStatus RequestStatus { get; set; }

        public virtual SourceType SourceType { get; set; }

        [ForeignKey("CreatedUserId")]
        public virtual User CreatedUser { get; set; }

        public virtual CustomerRequestType CustomerRequestType { get; set; }

        public virtual Department Department { get; set; }

        public virtual Rating Rating { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainTask> MainTask { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestEmail> RequestEmail { get; set; }

        public virtual ICollection<RequestStatusHistory> RequestStatusHistory { get; set; }
    }
}
