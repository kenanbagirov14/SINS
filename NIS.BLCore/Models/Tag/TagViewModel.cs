using System;
using NIS.BLCore.DTO;

namespace NIS.BLCore.Models.Tag
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public int? MainTaskId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public TaskDto MainTask { get; set; }
    }
}
