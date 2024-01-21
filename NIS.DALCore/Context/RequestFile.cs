namespace NIS.DALCore.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RequestFile")]
    public partial class RequestFile
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public int RequestId { get; set; }

        public string Description { get; set; }
    }
}
