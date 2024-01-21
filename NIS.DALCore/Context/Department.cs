namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Department")]
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            CustomerRequest = new HashSet<CustomerRequest>();
            CustomerRequestType = new HashSet<CustomerRequestType>();
            Project = new HashSet<Project>();
            //Users = new HashSet<User>();
            MainTask = new HashSet<MainTask>();
            InjuryTypes = new HashSet<InjuryType>();
            ChildDepartments = new HashSet<Department>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }

        public int? DepartmentPrefix { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }

        public string Description { get; set; }

        public int? DefaultUserId { get; set; }

        public int? ParentDepartmentId { get; set; }
        public int? CuratorUserId { get; set; }

        public string Email { get; set; }

        public string DepartmentType { get; set; }

        public int? Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerRequest> CustomerRequest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerRequestType> CustomerRequestType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project { get; set; }
        //[NotMapped]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<User> Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> ChildDepartments { get; set; }

        [ForeignKey("DefaultUserId")]
        public virtual User DefaultUser { get; set; }

        [ForeignKey("ParentDepartmentId")]
        public virtual Department ParentDepartment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainTask> MainTask { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InjuryType> InjuryTypes { get; set; }
    }
}
