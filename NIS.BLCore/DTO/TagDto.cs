using System;

namespace NIS.BLCore.DTO
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Description { get; set; }
        public int? MainTaskId { get; set; }
    }
}
