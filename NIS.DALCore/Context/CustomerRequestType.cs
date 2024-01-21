namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("CustomerRequestType")]
    public partial class CustomerRequestType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerRequestType()
        {
            CustomerRequest = new HashSet<CustomerRequest>();
            ChildRequestTypes = new HashSet<CustomerRequestType>();
        }

        public int Id { get; set; }

        public int? DepartmentId { get; set; }

        public int? ParentRequestTypeId { get; set; }

        public string Name { get; set; }

        public int ExecutionDay { get; set; }

        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }

        public int? SourceTypeId { get; set; }

        public string Alias { get; set; }
        public bool? AutoCreateTask { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerRequest> CustomerRequest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerRequestType> ChildRequestTypes { get; set; }

        public virtual CustomerRequestType ParentRequestType { get; set; }

        public virtual SourceType SourceType { get; set; }

        public virtual Department Department { get; set; }
    }
}
