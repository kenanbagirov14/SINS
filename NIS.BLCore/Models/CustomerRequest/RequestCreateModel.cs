using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Attachment;
using NIS.BLCore.Models.MainTask;

namespace NIS.BLCore.Models.CustomerRequest
{
    public class RequestCreateModel
    {
        public int CustomerRequestTypeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? UserId { get; set; }
        public int? RegionId { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerNumber { get; set; }
        public string ContractNumber { get; set; }

        public string AON { get; set; }
        public string Text { get; set; }
        public DateTime? StartDate { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public int SourceTypeId { get; set; } = 2;
        public string Email { get; set; }
        public string MailUniqueId { get; set; }


        public ICollection<TaskDto> MainTask { get; set; } = new List<TaskDto>();
        public List<AttachmentCreateModel> Attachment { get; set; }
    }
}
