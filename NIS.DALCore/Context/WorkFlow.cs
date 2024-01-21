namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("WorkFlow")]
    public partial class WorkFlow
    {
        public int Id { get; set; }

        public int CustomerRequestId { get; set; }

        public int DepartmentId { get; set; }

        public int Type { get; set; }

        public string ConditionKey { get; set; }

        public string ConditionValue { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }
    }
}
