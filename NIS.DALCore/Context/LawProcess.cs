namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LawProcess")]
    public partial class LawProcess
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime OrderDateTime { get; set; }

        public string OrderNo { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? InputDateTime { get; set; }
        public string Court { get; set; }
        public string Judge { get; set; }

        public string Description { get; set; }

        public bool Final { get; set; }

        public double? Amount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }

        public int CustomerRequestId { get; set; }
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}
