using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Comment;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentCreateModel, Comment>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Comment, CommentCreateModel>();
            CreateMap<CommentCreateModel, Comment>(); 
            CreateMap<Comment, CommentUpdateModel>();
            CreateMap<CommentUpdateModel, Comment>();
            CreateMap<Comment, CommentDeleteModel>();
            CreateMap<CommentDeleteModel, Comment>();
        }
       
    }
}
