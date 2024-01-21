using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Attachment;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<AttachmentCreateModel, Attachment>();
            CreateMap<Attachment, AttachmentViewModel>();
            CreateMap<AttachmentCreateModel, AttachmentViewModel>();
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Attachment, AttachmentCreateModel>();
            CreateMap<AttachmentCreateModel, Attachment>();
            CreateMap<Attachment, AttachmentUpdateModel>();
            CreateMap<AttachmentUpdateModel, Attachment>();
            CreateMap<Attachment, AttachmentDeleteModel>();
            CreateMap<AttachmentDeleteModel, Attachment>();
        }
       
    }
}
