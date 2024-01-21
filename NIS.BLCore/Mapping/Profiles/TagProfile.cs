using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Tag;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagCreateModel, Tag>();
            CreateMap<Tag, TagViewModel>();
            CreateMap<Tag, TagDto>();
            CreateMap<Tag, TagCreateModel>();
            CreateMap<TagCreateModel, Tag>();
            CreateMap<Tag, TagUpdateModel>();
            CreateMap<TagUpdateModel, Tag>();
            CreateMap<Tag, TagDeleteModel>();
            CreateMap<TagDeleteModel, Tag>();
        }
       
    }
}
