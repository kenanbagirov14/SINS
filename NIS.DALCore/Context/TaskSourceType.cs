namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TaskSourceType")]
    public partial class TaskSourceType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Description { get; set; }
    }
}
