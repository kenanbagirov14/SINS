using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NIS.DALCore.Context
{
  public  class CustomerRequestArchive
    {
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



        [NotMapped]
        public virtual ICollection<Attachment> Attachment { get; set; }
        [NotMapped]
        public virtual Region Region { get; set; }
        [NotMapped]
        public virtual RequestStatus RequestStatus { get; set; }
        [NotMapped]
        public virtual SourceType SourceType { get; set; }
        [NotMapped]
        [ForeignKey("CreatedUserId")]
        public virtual User CreatedUser { get; set; }
        [NotMapped]
        public virtual CustomerRequestType CustomerRequestType { get; set; }
        [NotMapped]
        public virtual Department Department { get; set; }
        [NotMapped]
        public virtual Rating Rating { get; set; }

        [NotMapped]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainTask> MainTask { get; set; }

        [NotMapped]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestEmail> RequestEmail { get; set; }
        [NotMapped]
        public virtual ICollection<RequestStatusHistory> RequestStatusHistory { get; set; }
    }
}
